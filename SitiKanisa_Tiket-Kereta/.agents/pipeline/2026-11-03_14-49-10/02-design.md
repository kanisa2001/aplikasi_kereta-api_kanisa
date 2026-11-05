# Technical Design — Perbaikan & Optimalisasi Sistem CRUD (SitiKanisa_Tiket-Kereta)

## 0. Status revisi

Tidak ada `design-review.json` di run directory ini, sehingga ini adalah desain awal (bukan revisi). Seluruh keputusan di bawah diturunkan dari `01-requirements.md` dan dari pembacaan langsung setiap file sumber di repo. Bagian "Respons terhadap review" di akhir dokumen mencatat hal itu.

---

## 1. Overview

Aplikasi ini adalah WinForms .NET Framework 4.7.2 untuk pemesanan tiket kereta api dengan MySQL `dbkereta_api`. Pekerjaannya adalah **perbaikan, bukan pembuatan ulang**: memperbaiki logika CRUD pada sembilan form, menutup celah SQL Injection, memperbaiki manajemen koneksi `db.cs`, menegakkan hak akses, dan membuat `README.md`. UI, layout, tema, nama kontrol, dan struktur DataGridView yang ada **tidak diubah**.

Setelah membaca seluruh kode, pola yang sudah mapan di seluruh form adalah: DataGridView **unbound** (manual `Rows.Clear()` + `Rows.Add()`), primary key disimpan di `Cells[0]` (disembunyikan via `Columns["..."].Visible = false` di `_Load` bila perlu), tombol Edit/Hapus berupa kolom gambar/teks pada indeks tetap, handler `CellClick`/`CellContentClick` mengambil PK dari `Cells[0]`, dan seluruh akses DB melalui `db.crud($"... {var} ...")` lalu membaca `db.ds.Tables[0]`. Masalahnya seragam: (1) seluruh SQL memakai interpolasi string → SQL Injection; (2) tidak ada `try-catch` di form mana pun (kecuali `Form2`); (3) `DataError` grid ditelan (`ex.Cancel = true`); (4) `db.cs` memakai satu `MySqlConnection static` yang tidak pernah ditutup eksplisit plus `DataSet`/`DataAdapter`/`Command` static bersama; (5) validasi input nyaris tidak ada (hanya `FPESANAN` punya); (6) beberapa bug logika per-baris (`FPENUMPANG` edit `SELECT *` tanpa `WHERE`, `FJADWAL.txttampildata_Click` memakai kolom `rute.id_kereta` yang tidak ada, pencarian `FPETUGAS`/`FROLE` kehilangan kolom PK).

Strategi desain: **pertahankan kontrak `db.crud(sql) → db.ds.Tables[0]`** agar setiap form tetap berfungsi, namen reimplementasi internal `db.cs` agar tiap operasi membuka/menutup koneksinya sendiri via `using` dan mendukung parameterized query + transaksi. Tambahkan helper terpisah untuk validasi dan logging agar pola konsisten antar form tanpa menambah duplikasi. Lalu perbaiki tiap form secara terarah: parameterisasi, validasi, try-catch, refresh grid, dan perbaikan bug spesifik. Pendekatan ini menyentuh permukaan seminimal mungkin (struktur file, nama kontrol, dan UI tidak berubah) sambil menutup semua kelemahan yang diminta.

## 2. Technology stack (terkunci setelah disetujui)

- Bahasa: C# pada .NET Framework 4.7.2 (WinForms), MSBuild/Visual Studio.
- UI: Guna.UI2 2.0.4.8 (kontrol `Guna2DataGridView`, `Guna2Button`, `Guna2TextBox`, `Guna2ComboBox`).
- Database: MySQL via MySql.Data 26.7.0; database `dbkereta_api`.
- Reporting: CrystalDecisions 13.0.4000 (viewer `Form2` memuat `pemesanan.rpt`; `FJADWAL.btncetak` mengekspor `Report_Jadwal.rpt` ke PDF). File `.rpt` tidak ada di repo (pre-existing) dan **di luar scope** untuk dibuat/diedit; yang ada hanya dijaga agar tidak rusak lebih lanjut.
- Logging: `log4net 2.0.10` sudah direferensikan tapi belum dikonfigurasi/dipakai. Lihat keputusan di §5 — kita tidak memakai log4net, melainkan logger internal ringkas.

Tidak ada dependensi ditambah/diganti; `packages.config` dan referensi `csproj` tidak berubah kecuali satu `<Compile Include>` baru untuk `Validasi.cs` (§6).

## 3. Keputusan kunci

**K1. Pertahankan kontrak `db.crud(sql)` → `db.ds.Tables[0]`.** Semua form membaca hasil lewat `db.ds.Tables[0]` (diverifikasi via pencarian: dipakai di `Form1`, `RIWAYAT`, `FJADWAL`, `FKERETA`, `FPESANAN`, `FPENUMPANG`, `FRUTE`, `FROLE`, `FPETUGAS`). Field static `koneksi`, `da`, `perintah` **tidak direferensikan form mana pun** (pencarian `db\.(koneksi|da|perintah)` mengembalikan nol hasil), sehingga aman dihapus. Karena itu `db.cs` tetap menyimpan `public static DataSet ds` untuk kompatibilitas, tetapi setiap operasi sekarang memakai koneksi lokal `using`. Pendekatan alternatif (mengganti seluruh form ke `DataTable` return-value) ditolak karena menyentuh terlalu banyak baris dan melanggar prinsip "jangan membuat ulang".

**K2. Parameterized query di semua tempat, termasuk login dan `LIKE`.** Setiap `db.crud($"... '{var}' ...")` diganti `db.crud(sql, parameter)` dengan `MySqlParameter`. Untuk `LIKE`, pola `WHERE x LIKE '%' + @q + '%'` (nilai parameter sudah mengandung kata kunci, atau gunakan `CONCAT('%', @q, '%')`). Ini menutup SQL Injection (FR8). Log query di level `AppLog` hanya mencatat teks SQL, **bukan nilai parameter**, agar tidak membocorkan data.

**K3. Koneksi per-operasi via `using`.** Hapus `static MySqlConnection koneksi`. Tiap method di `db.cs` membuat `new MySqlConnection(ConnectionString)` di dalam `using`, sehingga open/close/dispose otomatis. Ini menghilangkan connection leak dan risiko "connection already open" (FR7.1, FR7.3, AC9). `DataSet` static masih dipakai untuk kompatibilitas; karena UI WinForms berjalan single-threaded dan form ditampilkan satu per satu di panel (`KF.UntukForm` membersihkan panel lalu menampilkan satu form), kondisi race pada `ds` bersama (FR7.2) tidak terjadi dalam praktik — dan code path baru (`Fill`) mengembalikan `DataTable` segar bila nanti diperlukan. Risiko ini dicatat, bukan diabaikan.

**K4. Transaksi untuk operasi atomik `FPESANAN`.** Cek sisa kapasitas + insert pemesanan dibungkus satu transaksi (FR10.2) agar tidak ada oversell di sela dua query. Disediakan helper `db.EksekusiTransaksi(...)` dan overload `ExecNonQuery/Scalar` yang menerima `MySqlConnection`+`MySqlTransaction` eksplisit.

**K5. Hak akses: tolak secara default.** `MENU.CekAkses` sudah menolak di logika (tetap dipertahankan). Inkonsistensi (FR9.2) ada di `TerapkanAksesRole`: role tak dikenal saat ini menampilkan **semua** tombol (fallback) padahal `CekAkses` menolak. Diperbaiki dengan menambah cabang `else` yang menyembunyikan **semua** tombol untuk role tak dikenal. Kebijakan akses Admin/Super Admin tidak diubah sama sekali.

**K6. Logging ringkas internal, bukan log4net.** `log4net` sudah direferensikan tetapi tidak terkonfigurasi (tidak ada `[assembly: XmlConfigurator]` maupun konfigurasi di `App.config`). Mengkonfigurasi log4net menambah churn di `App.config`/`AssemblyInfo.cs` dan risiko. Sebagai gantinya, dibuat static class `db.AppLog` yang menulis ke `%LocalAppData%\SitiKanisa_Tiket-Kereta\app.log` (satu baris per entri: timestamp, level, pesan, pesan exception). Ini cukup untuk menelusuri error (FR6.4), mandiri, dan tidak butuh konfigurasi. `log4net` tetap tersedia bila nanti ingin dipakai. Pilihan ini didasarkan pada minimnya permukaan perubahan dan keandalan; jika reviewer lebih memilih log4net, itu adalah pengganti drop-in yang terbatas pada §5.

**K7. Validasi terpusat di `Validasi.cs`.** Agar pesan dan pola konsisten antar form (NFR keterbacaan), dibuat static class `Validasi` dengan `HarusIsi`, `CekInteger`, `CekWaktu`. Form memanggilnya sebelum menyimpan. Alternatif (inline `if` di tiap form) ditolak karena menimbulkan duplikasi dan pesan tidak konsisten. File baru `Validasi.cs` ditambahkan ke `csproj` (satu `<Compile Include>`).

## 4. File yang dimodifikasi / ditambahkan

| File | Aksi | Ringkasan |
|---|---|---|
| `db.cs` | Modifikasi besar (struktur kelas) | Koneksi per-operasi, `crud(sql,params)`, `Fill`, `ExecNonQuery`, `Scalar`, `EksekusiTransaksi`, `AppLog`, `ConnectionString`/`DbUser`/`DbPassword`. Hapus field static `koneksi`/`da`/`perintah`. Pertahankan `ds`. |
| `Validasi.cs` | **Baru** | Static class `Validasi` (`HarusIsi`, `CekInteger`, `CekWaktu`). |
| `SitiKanisa_Tiket-Kereta.csproj` | Modifikasi kecil | Tambah `<Compile Include="Validasi.cs" />`. |
| `Form1.cs` | Modifikasi | Parameterisasi login, try-catch DB. |
| `MENU.cs` | Modifikasi kecil | `TerapkanAksesRole` tolak-default; `CekAkses` tetap. |
| `FPETUGAS.cs` | Modifikasi | CRUD parameterized, validasi, try-catch, perbaikan pencarian & handler ganda. |
| `FROLE.cs` | Modifikasi | CRUD parameterized, validasi, try-catch, pencarian sertakan `idRole`, bersihkan `LBLID` setelah ubah. |
| `FKERETA.cs` | Modifikasi | CRUD parameterized, validasi numerik, try-catch, cek hasil lookup. |
| `FRUTE.cs` | Modifikasi | CRUD parameterized, validasi, try-catch, cek hasil lookup, asal≠tujuan. |
| `FJADWAL.cs` | Modifikasi | CRUD parameterized, validasi waktu, try-catch, perbaikan `txttampildata_Click` (hapus `rute.id_kereta`), penjaga `RowIndex`, jaga Crystal Report. |
| `FPENUMPANG.cs` | Modifikasi | CRUD parameterized, validasi, try-catch, **perbaikan bug edit `SELECT *` tanpa `WHERE`**. |
| `FPESANAN.cs` | Modifikasi | Parameterized, **transaksi** cek-kapasitas+insert, perbaikan `txtnama_TextChanged` (isi `nik` bukan `idpenumpang`), jaga print. |
| `RIWAYAT.cs` | Modifikasi kecil | Parameterisasi `txtcari`, aktifkan kolom "Print" → buka `Form2`, read-only tetap. |
| `Form2.cs` | Modifikasi kecil | `SetDatabaseLogon(db.DbUser, db.DbPassword)` (memusatkan kredensial, nilai sama). |
| `FGERBONG.cs` | Tidak diubah | Stub kosong, tidak terhubung menu, tidak ada tabel `gerbong`. Didokumentasikan statusnya (Out of Scope). |
| `README.md` (di root workspace luar) | **Baru** | Dokumentasi A–F. |

`README.md` ditempatkan di **root workspace luar** `c:\Users\Panji Hasbi Muhamad\source\repos\SitiKanisa_Tiket-Kereta\README.md` (sesuai frasa "direktori root workspace" pada permintaan; bukan di folder project bagian dalam). File `.Designer.cs` dan `.resx` **tidak disentuh** (UI tidak berubah); kalaupun ada penyesuaian handler, dilakukan dengan mengarahkan ulang method yang sudah ada, bukan mengedit wiring designer.

## 5. `db.cs` — rancangan konkret (inti perbaikan)

Kontrak yang dipertahankan: `db.crud(sql)` mengisi `db.ds.Tables[0]`; `db.ds` tetap `public static`. Anggota baru/lama:

```
public static class db  // tetap class (bukan static class) agar ds bisa instance field
{
    // Sumber tunggal kredensial — nilai TIDAK diubah dari yang berjalan
    public const string ConnectionString = "server=127.0.0.1;uid=root;pwd=;database=dbkereta_api;";
    public const string DbUser = "root";
    public const string DbPassword = "";

    public static DataSet ds = new DataSet();   // kompatibilitas: Tables[0] = hasil terakhir

    // --- Back-compat: hasil di db.ds.Tables[0] ---
    public static void crud(string sql, params MySqlParameter[] prms)
    {
        try
        {
            ds.Tables.Clear();
            ds.Tables.Add(Fill(sql, prms));
        }
        catch (Exception ex) { AppLog.Error("crud gagal", ex); throw; }
    }

    // --- API baru (direkomendasikan) ---
    public static DataTable Fill(string sql, params MySqlParameter[] prms) { /* using conn, using cmd, Fill tabel baru */ }
    public static int    ExecNonQuery(string sql, params MySqlParameter[] prms) { /* using conn+cmd, ExecuteNonQuery */ }
    public static object Scalar(string sql, params MySqlParameter[] prms) { /* using conn+cmd, ExecuteScalar */ }

    // --- Dalam transaksi ---
    public static DataTable Fill(MySqlConnection c, MySqlTransaction t, string sql, params MySqlParameter[] prms) { ... }
    public static int    ExecNonQuery(MySqlConnection c, MySqlTransaction t, string sql, params MySqlParameter[] prms) { ... }
    public static object Scalar(MySqlConnection c, MySqlTransaction t, string sql, params MySqlParameter[] prms) { ... }

    // --- Transaksi atomik ---
    public static void EksekusiTransaksi(Action<MySqlConnection, MySqlTransaction> aksi)
    {
        using (var conn = new MySqlConnection(ConnectionString))
        {
            conn.Open();
            using (var tr = conn.BeginTransaction())
            {
                try { aksi(conn, tr); tr.Commit(); }
                catch (Exception ex) { try { tr.Rollback(); } catch { } AppLog.Error("transaksi di-rollback", ex); throw; }
            }
        }
    }

    // --- Logger ringkas ---
    public static class AppLog
    {
        private static readonly string _path =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         "SitiKanisa_Tiket-Kereta", "app.log");
        public static void Info(string msg)  { Write("INFO ", msg, null); }
        public static void Error(string msg, Exception ex = null) { Write("ERROR", msg, ex); }
        private static void Write(string lvl, string msg, Exception ex) { /* append 1 baris; buat direktori bila perlu; Debug.WriteLine */ }
    }
}
```

Catatan penting:
- Setiap method `using (var conn = new MySqlConnection(ConnectionString)) { conn.Open(); ... }` — koneksi ditutup/dispose otomatis bahkan jika exception. Tidak ada lagi koneksi persisten.
- `crud` tetap `void` dan mengisi `ds.Tables[0]` agar semua pemanggilan `db.crud(...)` lama + `db.ds.Tables[0]` tetap valid. `params MySqlParameter[]` memungkinkan `db.crud("SELECT ...")` tanpa argumen (kompatibel penuh).
- `Console.WriteLine(queri)` dihapus (membocorkan SQL+nilai ke console); diganti `AppLog.Info` yang hanya mencatat SQL pada level debug.
- Exception di-rethrow setelah dicatat, sehingga form (yang kini punya try-catch) menampilkan pesan informatif. `crud` tidak menelan error.
- `db.ds.Clear()` yang masih dipanggil di beberapa form (mis. `FPENUMPANG`, `FROLE`, `FKERETA`, `FRUTE`) menjadi no-op yang tidak berbahaya karena `crud` sudah membersihkan; baris itu bisa dibiarkan (meminimalkan churn) atau dirapikan. Tidak menyebabkan bug.
- `Form2.SetDatabaseLogon("root","")` diubah ke `SetDatabaseLogon(db.DbUser, db.DbPassword)` — nilai identik, sekarang terpusat (FR7.4).

Skema DB diasumsikan seperti pada `01-requirements.md` (Asumsi 1) dan **harus dikonfirmasi terhadap database saat implementasi** sebelum mengubah query (lihat §11). Tidak ada file `.sql` di repo. Kolom yang dipakai: `petugas(idp,username,password,idrole,status)`, `role(idRole,nama_role,keterangan)`, `kereta(idkereta,nama_kereta,id_jenis_kereta,kapasitas,status,harga)`, `jenis_kereta(id_jenis_kereta,nama_jenis)`, `stasiun(id_stasiun,nama_stasiun)`, `rute(id_rute,id_stasiun_asal,id_stasiun_tujuan)`, `jadwal(idjadwal,id_kereta,id_rute,tanggal,jam_berangkat,jam_tiba,Harga)`, `penumpang(idpenumpang,nama,nik,jenis_kelamin,no_hp)`, `pemesanan(id_pemesanan,id_jadwal,nama_pemesan,no_identitas,no_telp,jumlah_tiket,metode_bayar,tanggal_pemesanan,harga,total_harga)`. Catat inkonsistensi penamaan: `petugas.idrole` (r kecil) vs `role.idRole` (R besar) — MySQL Windows case-insensitive untuk nama kolom sehingga aman, namun parameter query memakai nama kolom persis seperti di tabel.

## 6. `Validasi.cs` — helper validasi

```
public static class Validasi
{
    // true jika semua terisi; bila ada kosong → MessageBox "Field <label> wajib diisi" + false
    public static bool HarusIsi(params (string label, string nilai)[] input);

    // true jika integer dan >= min; bila tidak → MessageBox "Field <label> harus berupa angka >= <min>" + false
    public static bool CekInteger(string nilai, string label, int min = 0);

    // true jika "H:mm" atau "HH:mm" valid; bila tidak → MessageBox "Format <label> harus JJ:MM" + false
    public static bool CekWaktu(string hhmm, string label);
}
```

Pemakaian: `if (!Validasi.HarusIsi(("Username", txtuser.Text), ("Password", txtpass.Text))) return;`. Pesan spesifik per field (FR5.4). Semua method murni → unit-testable (§9).

## 7. Desain per form

Konvensi baca untuk setiap form: grid unbound; `Cells[0]` = PK; Edit pada indeks kolom `E`; Hapus pada `H`; handler klik memulai dengan `if (e.RowIndex < 0) return;` (penjaga header-row, lihat §8). Setiap handler simpan/ubah/hapus dibungkus `try { ... } catch (Exception ex) { MessageBox.Show("...: " + ex.Message, ..., Error); db.AppLog.Error(...); }`. Setiap aksi sukses diakhiri `<grid>.tampildata()` (refresh) + `bersih()` (kosongkan form) + reset pemegang PK (`LBLID.Text = ""` / `label9.Text = ""`).

### 7.1 Form1.cs (Login) — semua role
- **Kontrol**: `txtuser`, `txtpass`, `btnlogin` (handler `guna2Button1_Click`).
- **Perubahan**: ganti interpolasi dengan parameter. Query tetap: `SELECT petugas.idp, petugas.username, petugas.status, role.nama_role FROM petugas INNER JOIN role ON petugas.idrole = role.idRole WHERE petugas.username = @u AND petugas.Password = @p`. Bungkus dalam `try-catch`: pada `MySqlException` → `MessageBox.Show("Gagal terhubung ke database: " + ex.Message, "Koneksi", OK, Error)` + `AppLog.Error` + return (tidak crash).
- **Validasi**: `HarusIsi(("Username", txtuser.Text), ("Password", txtpass.Text))` sebelum query; bila kosong → pesan + return.
- **Logika status** (`status == "Tidak Aktif"`) dan pengiriman `namaRole`/`namaUser` ke `MENU` **tidak diubah**. Hashing password di luar scope (Asumsi 6).

### 7.2 MENU.cs — hak akses
- `CekAkses(string[])` tetap apa adanya (sudah benar, menolak di logika).
- `TerapkanAksesRole`: tambah `else { sembunyikan semua guna2Button1..8 }` agar role tak dikenal → deny-by-default, konsisten dengan `CekAkses` (FR9.2). Kebijakan tombol Admin/Super Admin tidak diubah.
- Tidak ada perubahan mapping tombol↔handler atau array role.

### 7.3 FPETUGAS.cs — Super Admin (Petugas)
- **Grid** `tablepetugas`: `Cells[0]`=idp (kolom `Column6`, `Visible=false` di `_Load`); Edit `E=5`; Hapus `H=6`. **Pemegang PK**: `LBLID`.
- **Kontrol**: `txtuser`, `txtpass`, `cbrole` (diisi `role.nama_role` di `_Load`), `cbstatus` ("Aktif"/"Tidak Aktif"), `txtsearch`, `btnsimpan`, `btnubah`.
- **Read** `tampildata()`: `SELECT idp, username, role.nama_role, status FROM petugas INNER JOIN role ON petugas.idrole = role.idRole` → `Rows.Add(idp, no, user, role, status)`. Sudah benar, parameterisasi tidak perlu (tanpa input) — tetap pakai `crud(sql)` tanpa params.
- **Insert** (`btnsimpan_Click_1`, handler aktif): validasi `HarusIsi`; lookup `SELECT idRole FROM role WHERE nama_role=@r` dengan cek `Rows.Count==0` → "Role tidak ditemukan, buka ulang form" + return; `INSERT petugas(username,password,idrole,status) VALUES(@u,@p,@idr,@s)` via `ExecNonQuery`; `tampildata()` + `bersih()`. Handler kosong `btnsimpan_Click` dihapus isinya tetap dibiarkan (atau di-unwrap di designer) — yang penting hanya satu handler aktif yang melakukan insert; karena `btnsimpan_Click` sudah kosong, tidak ada double-insert saat ini, namun pastikan wiring designer hanya menempel **satu** handler `Click` ke `btnsimpan` (FR6.5/11.1).
- **Update** (`btnubah_Click_1`): cek `LBLID.Text == ""` → "Pilih baris yang akan diubah" + return (AC4); validasi; lookup idRole (cek count); `UPDATE petugas SET username=@u, password=@p, idrole=@idr, status=@s WHERE idp=@id` (`@id=LBLID.Text`); refresh + `bersih()` + `LBLID.Text=""` (tambahkan reset yang hilang).
- **Delete** (`tablepetugas_CellClick`, `H=6`): konfirmasi YesNo (sudah ada); `DELETE FROM petugas WHERE idp=@id`; tangani FK rejection (§8).
- **Pencarian** (FR11.1): saat ini ada **dua** handler — `guna2TextBox1_TextChanged` (SELECT tanpa `idp` lalu baca `baris["idp"]` → `KeyNotFoundException`/crash) dan `txtsearch_TextChanged` (SELECT dengan `idp` tapi **memuat ulang semua**, tidak memfilter). Solusi defensif yang tidak menyentuh designer: jadikan **kedua** method memanggil satu `private void CariData()` yang melakukan `SELECT idp, username, role.nama_role, status FROM petugas INNER JOIN role ON ... WHERE username LIKE CONCAT('%', @q, '%')` lalu `Rows.Add(idp, no, user, role, status)`. Dengan demikian, apa pun yang ter-wiring ke `txtsearch.TextChanged`, hasilnya benar (mencakup PK + memfilter) dan tidak crash; double-fire hanya mengulang pembersihan grid (idempoten, tak berbahaya). Implementer boleh merapikan designer menjadi satu handler bila diinginkan (opsional).

### 7.4 FROLE.cs — Super Admin (Role)
- **Grid** `dgvrole`: `Cells[0]`=idRole (kolom `Column3`, `Visible=false`); Edit `E=4`; Hapus `H=5`. **Pemegang PK**: `LBLID`.
- **Kontrol**: `txtrole`, `txtket`, `txtsearch`.
- **Read** `tampildata()`: `SELECT idRole, nama_role, keterangan FROM role` → `Rows.Add(idr, no, role, ket)`. Benar.
- **Insert**: `HarusIsi`; `INSERT role(idRole, nama_role, keterangan) VALUES(NULL, @r, @k)`; refresh + `bersih()`.
- **Update** (`btnubah_Click_1`): cek `LBLID`; validasi; `UPDATE role SET nama_role=@r, keterangan=@k WHERE idRole=@id` (normalisasi `idrole`→`idRole`); **tambahkan** `bersih()` + `LBLID.Text=""` setelah update (FR11.2).
- **Delete**: konfirmasi; `DELETE FROM role WHERE idRole=@id`; tangani FK (role dipakai petugas → ditolak DB).
- **Pencarian** (FR11.2): `guna2TextBox1_TextChanged` saat ini `Rows.Add(no, role, ket)` (3 sel, **tanpa idRole** → `Cells[0]` bergeser ke "no", edit/hapus membaca PK salah). Perbaiki: `SELECT idRole, nama_role, keterangan FROM role WHERE nama_role LIKE CONCAT('%', @q, '%')` dan `Rows.Add(idr, no, role, ket)` (4 sel, persis seperti `tampildata`).

### 7.5 FKERETA.cs — Super Admin (Kereta)
- **Grid** `tablekereta`: `Cells[0]`=idkereta (tampil, tidak disembunyikan di `_Load` — UI tetap); Edit `E=7`; Hapus `H=8`. **Pemegang PK**: `LBLID`.
- **Kontrol**: `txtnmkereta`, `cmbjenis` (`jenis_kereta.nama_jenis` via `isiJenis` + `DropDown`), `cmbstatus` (3 status), `txtkursi` (kapasitas), `txtharga`, `txtsearch`.
- **Read** `tampildata()`: JOIN `kereta`×`jenis_kereta` → `Rows.Add(id, no, nmk, jns, krs, stts, hrg)`. Benar.
- **Insert**: `HarusIsi` + `CekInteger(txtkursi, "Kapasitas", 1)` + `CekInteger(txtharga, "Harga", 0)`; lookup `id_jenis_kereta` (cek count==0 → "Jenis kereta tidak ditemukan" + return — **tambahkan** cek yang hilang di `btnsimpan`); `INSERT kereta(idkereta, nama_kereta, id_jenis_kereta, kapasitas, status, harga) VALUES(NULL, @nmk, @idj, @krs, @st, @hrg)`; refresh + `bersih()`.
- **Update** (`btnubah`): cek `LBLID`; validasi; lookup jenis (cek count, sudah ada); `UPDATE kereta SET ... WHERE idkereta=@id`; refresh + `bersih()` + `LBLID.Text=""`.
- **Edit isi form** (`E=7`): kini `SELECT * FROM kereta INNER JOIN jenis_kereta ... WHERE idkereta=@id`; isi `cmbjenis.SelectedItem = jns` (pertahankan). Hapus blok `if (db.ds != null) db.ds.Clear();` yang berlebih (opsional, no-op).
- **Delete** (`H=8`): konfirmasi; `DELETE FROM kereta WHERE idkereta=@id`; FK (dipakai jadwal) → ditolak DB.
- **Pencarian**: sudah menyertakan `idkereta` di `Cells[0]` (benar); parameterisasi saja `LIKE CONCAT('%', @q, '%')`.

### 7.6 FRUTE.cs — Super Admin (Rute)
- **Grid** `tablerute`: `Cells[0]`=id_rute (tampil); Edit `E=4`; Hapus `H=5`. **Pemegang PK**: `LBLID`. Handler `tablerute_CellClick_1` sudah punya penjaga `baris<0` (pertahankan).
- **Kontrol**: `cbasal`, `cbtujuan` (`stasiun.nama_stasiun`), `txtsearch`.
- **Read** `tampildata()`: JOIN `rute`×`stasiun` (asal/tujuan) → `Rows.Add(idr, no, asal, tujuan)`. Benar.
- **Insert** (`btnsimpan`): `HarusIsi`; tambah validasi `cbasal.Text == cbtujuan.Text` → "Stasiun asal dan tujuan tidak boleh sama" + return; lookup id_stasiun asal & tujuan (cek count masing-masing, sudah ada); `INSERT rute(id_rute, id_stasiun_asal, id_stasiun_tujuan) VALUES(NULL, @asal, @tuj)`; refresh + `bersih()`.
- **Update** (`btnubah`): cek `LBLID`; validasi + asal≠tujuan; lookup (cek count — **tambahkan** yang hilang); `UPDATE rute SET id_stasiun_asal=@asal, id_stasiun_tujuan=@tuj WHERE id_rute=@id`; refresh + `bersih()` + `LBLID.Text=""`.
- **Delete** (`H=5`): konfirmasi; `DELETE FROM rute WHERE id_rute=@id`; FK (dipakai jadwal) → ditolak DB.
- **Pencarian**: menyertakan `id_rute` di `Cells[0]` (benar); parameterisasi.

### 7.7 FJADWAL.cs — Admin (Jadwal)
- **Grid** `tablejadwal`: 10 kolom — `0`=idjadwal (kolom `Column1`, header "IDJ", tampil); `1`=Nama Kereta; `2`=Stasiun Awal; `3`=Stasiun Tujuan; `4`=Tgl Berangkat; `5`=Jam Berangkat; `6`=Jam Tiba; `7`=Harga; `8`=Ubah (image, `E=8`); `9`=Hapus (image, `H=9`). **Pemegang PK**: `label9` (`Visible=false`). Wiring grid `CellContentClick` → `guna2DataGridView1_CellContentClick_1` (terkonfirmasi terpasang di Designer — bukan bug, namanya saja buruk). Jangan ubah nama handler/wiring.
- **Kontrol**: `cmbkereta` (DropDown load `nama_kereta`; `SelectedIndexChanged` load `harga`+`kapasitas`), `cmbrute` (DropDown load "id_rute - asal - tujuan"; `SelectedIndexChanged` isi `txtstaw`/`txtsttuj`), `dateTimePicker1` (tanggal), `txtberangkat`, `txttiba` (jam teks, saat ini `+":00"`), `txtharga`, `txtkapasitas` (dari lookup kereta), `txtstaw`/`txtsttuj` (read-only, `txtsttuj.Enabled=false`), `txtsearch`, `btnsimpan`, `btnubah` (wired ke `txtubah_Click`), `txttampildata` (tombol kecil image, wired ke `txttampildata_Click`), `btncetak` (wired ke `guna2Button1_Click`, Crystal Report export).
- **Read** `tampildata()`: JOIN `jadwal`×`kereta`×`rute`×`stasiun` → `Rows.Add(idj, nm, sa, st, tggl, jb, jt, h)`. Benar.
- **Insert** (`btnsimpan_Click`): validasi `HarusIsi` + `CekWaktu(txtberangkat, "Jam Berangkat")` + `CekWaktu(txttiba, "Jam Tiba")` + `CekInteger(txtharga, "Harga", 0)`; jam disusun sebagai `@jb = txtberangkat.Text + ":00"` hanya setelah `CekWaktu` lolos (FR5.3); lookup `idkereta` by `nama_kereta` (cek count, sudah ada); `idrute = cmbrute.Text.Split('-')[0].Trim()` (sumber tunggal yang benar — dari combo, bukan lookup nama stasiun); validasi rute ada (cek count, sudah ada); `INSERT jadwal(idjadwal, id_kereta, id_rute, tanggal, jam_berangkat, jam_tiba, Harga) VALUES(NULL, @idk, @idr, @tgl, @jb, @jt, @hrg)` dengan `@tgl=dateTimePicker1.Value.ToString("yyyy-MM-dd")`; refresh + `bersih()`.
- **Update** (`txtubah_Click`): cek `label9.Text==""` → "Pilih baris" + return; validasi sama; `UPDATE jadwal SET id_kereta=@idk, id_rute=@idr, tanggal=@tgl, jam_berangkat=@jb, jam_tiba=@jt, Harga=@hrg WHERE idjadwal=@id`; refresh + `bersih()` + `label9.Text=""`.
- **Perbaikan bug `txttampildata_Click` (FR3.4/11.4)**: saat ini melakukan `SELECT rute.id_rute ... WHERE rute.id_kereta = @idkereta AND asal.nama_stasiun=@staw AND tujuan.nama_stasiun=@sttuj` — kolom `rute.id_kereta` **tidak ada** di tabel `rute`, sehingga query melempar `MySqlException` → tidak ada try-catch → crash. Karena `cmbrute` sudah memegang id_rute yang benar (format "id - asal - tujuan"), perbaikan terbersih: **hapus query rusak itu** dan buat `txttampildata_Click` memanggil method bersama `private void SimpanUbahJadwal()` yang sama dengan `txtubah_Click` (parse `idrute` dari `cmbrute.Text`, lalu `UPDATE ... WHERE idjadwal=label9.Text`). Dengan demikian tombol "tampil data" menjadi update yang benar tanpa query rusak, UI tidak berubah, dan tidak ada duplikasi logika. (Bila reviewer merasa tombol ini redundan dengan `btnubah`, pendapat itu benar, namun menghapus tombol = mengubah UI → dilarang; jadi dibiarkan dan dirapikan.)
- **Edit isi form** (`guna2DataGridView1_CellContentClick_1`, `E=8`): tambah `if (e.RowIndex < 0) return;`; `SELECT jadwal.idjadwal, kereta.nama_kereta, rute.id_rute, asal.nama_stasiun AS stasiun_asal, tujuan.nama_stasiun AS stasiun_tujuan, jadwal.tanggal, jadwal.jam_berangkat, jadwal.jam_tiba, jadwal.Harga FROM ... WHERE jadwal.idjadwal=@idj`; isi `label9.Text`, `cmbkereta.Text`, `cmbrute.Text` ("id - asal - tujuan"), `txtstaw`/`txtsttuj`, `dateTimePicker1.Value=Convert.ToDateTime(tanggal)`, `txtberangkat`/`txttiba`/`txtharga`. Parameterisasi; pertahankan pola reload combo yang ada (cukup berfungsi).
- **Delete** (`H=9`): `if (e.RowIndex < 0) return;`; konfirmasi; `DELETE FROM jadwal WHERE idjadwal=@id`; FK (dipakai pemesanan) → ditolak DB; refresh + `bersih()` + `label9.Text=""`.
- **Pencarian**: menyertakan `idjadwal` di `Cells[0]` (benar); parameterisasi.
- **Crystal Report `btncetak`**: bungkus `guna2Button1_Click` dalam `try-catch` (saat ini tidak ada → crash jika `Report_Jadwal.rpt` tidak ada); tangani `FileNotFound` dengan pesan "File laporan tidak ditemukan" + log. Jangan buat/mengedit `.rpt` (Out of Scope). Fitur ini pre-existing dan mungkin sudah tidak berfungsi penuh karena `.rpt` tak ada di repo — dicatat di README; yang dijaga hanya agar tidak crash.

### 7.8 FPENUMPANG.cs — Admin + Super Admin (Penumpang)
- **Grid** `tablepenumpang`: `Cells[0]`=idpenumpang; Edit `E=5`; Hapus `H=6`. **Pemegang PK**: `LBLID`. (`cmdkelamin` = combo Perempuan/Laki-Laki; `txtindetitas` = nik; `txttelp` = no_hp — nama kontrol dipertahankan apa adanya termasuk typo.)
- **Read** `tampildata()`: `SELECT idpenumpang, nama, nik, jenis_kelamin, no_hp FROM penumpang` → `Rows.Add(idp, nama, identitas, jnskelamin, telp)`. Benar.
- **Insert**: `HarusIsi`; `INSERT penumpang(idpenumpang, nama, nik, jenis_kelamin, no_hp) VALUES(NULL, @nama, @nik, @jk, @hp)`; refresh + `bersih()`.
- **Update** (`btnubah`): cek `LBLID`; validasi; `UPDATE penumpang SET nama=@nama, nik=@nik, jenis_kelamin=@jk, no_hp=@hp WHERE idpenumpang=@id`; refresh + `bersih()` + `LBLID.Text=""`.
- **Bug kritis edit (FR3.3/11.3)**: `tablepenumpang_CellClick` saat `E=5` melakukan `SELECT * FROM penumpang` **tanpa `WHERE`**, lalu loop semua baris → form terisi **baris terakhir** dan `LBLID` diisi idpenumpang baris terakhir (bukan baris yang diklik). Perbaiki: `SELECT idpenumpang, nama, nik, jenis_kelamin, no_hp FROM penumpang WHERE idpenumpang=@id` dengan `@id=tablepenumpang.Rows[baris].Cells[0].Value`; isi form dari **satu** baris itu; `LBLID.Text=@id`. Hapus blok `if (db.ds != null) db.ds.Clear();` yang berlebih.
- **Delete** (`H=6`): `if (e.RowIndex < 0) return;`; konfirmasi (sudah ada); `DELETE FROM penumpang WHERE idpenumpang=@id`. Catatan: `penumpang` **tidak punya FK keluar** (pemesanan menyimpan `no_identitas` sebagai teks bebas, bukan FK), jadi DB tidak akan menolak; itu perilaku yang ada dan dipertahankan.
- **Pencarian**: sudah menyertakan `idpenumpang` di `Cells[0]` (benar); parameterisasi `LIKE CONCAT('%', @q, '%')` pada nama/nik/jenis_kelamin/no_hp.

### 7.9 FPESANAN.cs — Admin (Pemesanan tiket)
- **Bukan grid-list**; form pemesanan tiket tunggal. `tampildata()` kosong (dibiarkan). **Pemegang PK jadwal**: `LBLIDJ` (tersembunyi). Kontrol: `txtnama` (autocomplete dari `penumpang.nama`), `txtidentitas`, `txttelfon`, `cmbrute` ("id_rute - asal - tujuan"), `txttgl` (DateTimePicker), `txtkereta`, `txtkelas`, `txtharga`, `txttersedia`, `txttiket`, `txttotal`, `cmbbayar` (Transfer/Gopay/Dana), `btnpesan`, `ptnprint` (PrintDocument preview).
- **`carijadwal()`** (`cmbrute_SelectedIndexChanged`): `SELECT jadwal.idjadwal, jadwal.tanggal, jadwal.Harga, jadwal.jam_berangkat, kereta.nama_kereta, jenis_kereta.nama_jenis FROM jadwal INNER JOIN kereta ... INNER JOIN jenis_kereta ... WHERE jadwal.id_rute=@idr AND jadwal.tanggal >= CURDATE() ORDER BY jadwal.tanggal, jadwal.jam_berangkat LIMIT 1`; isi `LBLIDJ`, `txttgl`, `txtkereta`, `txtkelas`, `txtharga`; panggil `hitungTersedia()`. Bila kosong → pesan "Jadwal untuk rute tersebut tidak ditemukan!" (sudah ada). Parameterisasi.
- **`hitungTersedia()`**: `SELECT kereta.kapasitas, COALESCE(SUM(pemesanan.jumlah_tiket),0) AS terpakai FROM jadwal INNER JOIN kereta ... LEFT JOIN pemesanan ON jadwal.idjadwal=pemesanan.id_jadwal WHERE jadwal.idjadwal=@idj GROUP BY kereta.kapasitas`; `tersedia=kapasitas-terpakai` (clamp `<0 → 0`); tulis `txttersedia`. Parameterisasi. Ini adalah **sumber kebenaran kapasitas**; `txttiket_TextChanged` memakai nilai cache ini untuk umpan balik UX.
- **`txttiket_TextChanged`** (UX): validasi `int` harga/tiket/tersedia; bila `tiket>tersedia` → pesan + clear (sudah ada); bila `tiket<=0` → clear total; else `total=harga*tiket`. Pertahankan — konsisten dengan `hitungTersedia` (FR1.3: satu rumus `kapasitas-terpakai` di kedua tempat; DB adalah otoritatif saat submit).
- **`btnpesan_Click`** (FR1.3, FR10.2): validasi kelengkapan (sudah ada); `idjadwal=LBLIDJ`; `int.TryParse(tiket)` + `>0` (sudah ada). **Bungkus cek-kapasitas + insert dalam transaksi**:
  `db.EksekusiTransaksi((conn, tr) => { object o = db.Scalar(conn, tr, capacitySql, new MySqlParameter("@idj", idjadwal)); ... hitung tersedia; if (jumlah > tersedia) throw new InvalidOperationException("Tiket tersedia hanya " + tersedia); db.ExecNonQuery(conn, tr, insertSql, params); });`
  - `capacitySql` = query `hitungTersedia` (kapasitas + SUM terpakai) untuk idjadwal itu.
  - `insertSql` = `INSERT pemesanan(id_pemesanan, id_jadwal, nama_pemesan, no_identitas, no_telp, jumlah_tiket, metode_bayar, tanggal_pemesanan, harga, total_harga) VALUES(NULL, @idj, @nm, @noid, @telp, @tkt, @byr, NOW(), @hrg, @ttl)`.
  - Pada `catch (InvalidOperationException bex)` → `MessageBox.Show(bex.Message)` (penolakan bisnis, **bukan** error log). Pada `catch (Exception ex)` → pesan error + `AppLog.Error`. Atomik: bila insert gagal, tidak ada perubahan; bila kapasitas tidak cukup, tidak ada insert. `hitungTersedia()` dipanggil ulang setelahnya untuk menyegarkan `txttersedia`.
- **Bug `txtnama_TextChanged` (FR11.5)**: saat ini `txtidentitas.Text = baris["idpenumpang"]` (PK numerik) dan `txttelfon = no_hp`. `no_identitas` pada `pemesanan` dan label cetak "No. Identitas" semestinya **NIK** (nomor identitas manusia), bukan PK DB. Perbaiki: `txtidentitas.Text = baris["nik"].ToString()`; `txttelfon.Text = baris["no_hp"]`. Query `SELECT idpenumpang, nama, nik, no_hp FROM penumpang WHERE nama=@nama` (parameterisasi). **Asumsi yang dicatat**: bila bisnis memang ingin menyimpan `idpenumpang` di `no_identitas`, balikkan; namun NIK adalah interpretasi yang benar untuk "No. Identitas" dan untuk data yang dicetak.
- **Print** (`ptnprint` → `printDocument1_PrintPage`): pertahankan apa adanya (cetak tiket via `PrintDocument`, harus tetap berfungsi, AC17). Tidak ada perubahan layout tiket.

### 7.10 RIWAYAT.cs — Admin + Super Admin (Riwayat)
- **Grid** `dgvriwayat`: read-only. Kolom `0`=id_pemesanan, `1`=nama, `2`=no_identitas, `3`=no_telp, `4`=asal, `5`=tujuan, `6`=tanggal, `7`=harga, `8`=tiket, `9`="Print" (teks). Tidak ada kolom Edit/Hapus.
- **Read** `tampildata()`: JOIN `pemesanan`×`jadwal`×`rute`×`stasiun` → `Rows.Add(idp, nm, noid, notlp, sawal, stujuan, tgl, hrg, tkt, "Print")`. Tanpa input → `crud(sql)` tanpa params sudah cukup.
- **Pencarian** `txtcari_TextChanged`: parameterisasi `LIKE CONCAT('%', @q, '%')` pada nama_pemesan/no_identitas/no_telp/asal/tujuan. Menyertakan `id_pemesanan` di `Cells[0]` (benar).
- **Perbaikan "Print" (FR11.6)**: `dgvriwayat_CellContentClick` saat ini kosong → kolom "Print" mati. `guna2Button1_Click` membuka `Form2` tanpa pemilihan baris. Solusi minimal non-breaking: aktifkan `dgvriwayat_CellContentClick` — bila `e.ColumnIndex == 9` (kolom Print) dan `e.RowIndex >= 0` → buka `Form2` (sama persis dengan tombol). Dengan demikian kolom mati menjadi berfungsi tanpa menambah logika cetak baru dan tanpa mengubah UI. `Form2` (viewer Crystal Report) tetap menampilkan `pemesanan.rpt` keseluruhan (tidak ada filter per-baris; mengedit `.rpt`/menambah parameter di luar scope). Perilaku ini didokumentasikan di README: "cetak per-baris belum diimplementasikan; kolom Print membuka report viewer keseluruhan."

### 7.11 FGERBONG.cs — Out of Scope
Stub kosong: hanya handler `DataError` dan `txtsearch_TextChanged` kosong; tidak ada menu yang membukanya; tidak ada tabel `gerbong` di kode. **Tidak diimplementasikan**; statusnya dicatat di README sebagai "belum terintegrasi, menunggu konfirmasi tabel/kebutuhan" (sesuai Asumsi 4 / Out of Scope).

## 8. Penanganan error (per operasi) & aturan umum

Aturan umum untuk **setiap** handler simpan/ubah/hapus/pilih-baris:
1. Awali `CellClick`/`CellContentClick` dengan `if (e.RowIndex < 0) return;` (mencegah `IndexOutOfRange` saat klik header) — saat ini hanya `FRUTE` punya penjaga ini; tambahkan ke `FPETUGAS`, `FROLE`, `FKERETA`, `FPENUMPANG`, `FJADWAL`.
2. Validasi dulu (`Validasi.*`); bila gagal → pesan spesifik + `return` (tidak menyentuh DB).
3. Untuk update, cek pemegang PK (`LBLID`/`label9`) tidak kosong; bila kosong → "Pilih baris yang akan diubah terlebih dahulu" + `return` (AC4).
4. Bungkus seluruh blok DB dalam `try { ... } catch (Exception ex) { MessageBox.Show(pesan + ex.Message, judul, OK, Error); db.AppLog.Error(pesan, ex); }`.
5. Pada sukses → `tampildata()` (refresh grid) + `bersih()` + reset pemegang PK.
6. `DataError` grid: ganti `ex.Cancel = true;` murni menjadi `ex.Cancel = true; db.AppLog.Info("DataError grid: " + ex.Exception.Message);` (masih menekan exception UI agar tidak crash, tapi mencatat akar masalah — FR6.2). Ini tetap per form karena tidak ada error data nyata pada grid unbound; pencegahan crash utama ada di try-catch handler.

Tabel kegagalan per kategori:

| Operasi | Kondisi gagal | Recoverable/Fatal | Diterima caller | Level log |
|---|---|---|---|---|
| Validasi input | field kosong / non-numerik / waktu salah | Recoverable | `MessageBox` spesifik, operasi batal | — |
| Lookup FK (role/jenis/stasiun/kereta/rute) | `Rows.Count==0` | Recoverable | "X tidak ditemukan" + batal | Info |
| Update tanpa pilih baris | pemegang PK kosong | Recoverable | "Pilih baris..." + batal | — |
| Delete dengan FK masih dipakai | `MySqlException` No. **1451** | Recoverable | "Data tidak dapat dihapus karena masih digunakan pada data lain." | Error |
| Duplicate/unique saat insert | `MySqlException` No. **1062** | Recoverable | "Data sudah ada / nilai sudah terpakai." | Error |
| Koneksi DB gagal | `MySqlException` (konektivitas) | Recoverable di UI | "Gagal terhubung ke database: <msg>" | Error |
| Query SQL salah | `MySqlException` umum | Recoverable di UI | "Terjadi kesalahan query: <msg>" | Error |
| Transaksi `FPESANAN` kapasitas | `InvalidOperationException` bisnis | Recoverable | "Tiket tersedia hanya N" | Info (bukan Error) |
| Transaksi `FPESANAN` DB | `Exception` lain | Recoverable di UI | pesan error + rollback otomatis | Error |
| File `.rpt` hilang (`FJADWAL`/`Form2`) | `FileNotFound` | Recoverable | "File laporan tidak ditemukan: <path>" | Info |

Tidak ada operasi yang fatal (mematikan aplikasi); semua ditangkap di level form, dicatat, dan dilaporkan. `Application.Exit()` tidak dipanggil oleh handler CRUD mana pun. Hanya `MENU.MENU_FormClosing` yang memanggil `Application.Exit()` saat menutup tanpa logout — perilaku yang ada, tidak diubah.

## 9. Aturan validasi input (per field)

| Form | Field | Aturan | Saat gagal |
|---|---|---|---|
| Form1 | username, password | wajib isi | pesan + fokus |
| FPETUGAS | username, password, cbrole, cbstatus | wajib isi | pesan + batal |
| FROLE | txtrole, txtket | wajib isi | pesan + batal |
| FKERETA | nama_kereta, cmbjenis, cmbstatus | wajib isi | pesan + batal |
| FKERETA | kapasitas (`txtkursi`) | `CekInteger` min 1 | pesan + batal (AC12) |
| FKERETA | harga (`txtharga`) | `CekInteger` min 0 | pesan + batal (AC12) |
| FRUTE | cbasal, cbtujuan | wajib isi; asal≠tujuan | pesan + batal |
| FJADWAL | cmbkereta, cmbrute | wajib terpilih | pesan + batal |
| FJADWAL | jam_berangkat/tiba | `CekWaktu` "H:mm"/"HH:mm" sebelum `+":00"` | pesan + batal (FR5.3) |
| FJADWAL | harga | `CekInteger` min 0 | pesan + batal |
| FPENUMPANG | nama, nik, jenis_kelamin, no_hp | wajibi isi | pesan + batal |
| FPESANAN | semua field (nama, identitas, telfon, rute, bayar) | wajib isi (sudah ada) | pesan + batal |
| FPESANAN | jumlah_tiket | `int` + `>0` (sudah ada) + `≤ tersedia` (DB otoritatif) | pesan + batal |

`nik`/`no_hp` diperlakukan sebagai **teks wajib isi** (bukan hard-numeric) agar tidak over-konstrain (NIK bisa sangat panjang). AC12 secara spesifik menyebut harga/kapasitas/tiket — itu yang divalidasi numerik.

## 10. Invariant & kepemilikan

- **PK benar di `Cells[0]`** — dimiliki tiap `tampildata()`/search form: wajib `SELECT` PK dan menaruhnya di sel pertama. Pencarian `FPETUGAS`/`FROLE` diperbaiki agar menyertakan PK (bug FR11.1/11.2).
- **Integritas FK saat delete** — dimiliki DB (constraint) + form menangkap `MySqlException 1451` → pesan ramah. Tidak ada cascade yang ditambahkan (skema tidak diubah).
- **Kapasitas tidak terlampaui** — dimiliki `FPESANAN.btnpesan` lewat query DB dalam transaksi (otoritatif); `txttiket_TextChanged` hanya UX.
- **Hak akses** — dimiliki `MENU.CekAkses` (logika) + `TerapkanAksesRole` (visibilitas, kini deny-by-default). Konsisten.
- **Validasi input** — dimiliki handler simpan/ubah tiap form memakai `Validasi.*`.
- **Siklus koneksi** — dimiliki `db.cs` (pola `using` per operasi). Tidak ada koneksi bocor.
- **Tidak ada SQL injection** — dimiliki `db.cs` API parameterized + form yang hanya mengirim `@param` (tidak ada interpolasi nilai). Verifikasi via peninjauan kode (AC13).
- **Refresh grid tanpa duplikasi** — dimiliki `tampildata()` yang `Rows.Clear()` dulu lalu `Rows.Add` dari hasil DB (bukan append manual yang menumpuk) → insert muncul sekali (AC1, AC10).

## 11. Edge cases

- Klik header grid (`RowIndex<0`) → ditangani penjaga awal (mencegah crash).
- Search dikosongkan → `LIKE CONCAT('%','',  '%')` setara `LIKE '%%'` → semua tampil (perilaku "reset" gratis; pertahankan).
- Lookup FK gagal (role/jenis/stasiun/kereta/rute tidak ditemukan, mis. combo belum dimuat) → cek `Rows.Count==0` + pesan + batal. Ditambahkan di `FPETUGAS` insert/update dan `FKERETA` insert (saat ini hilang); sudah ada di `FKERETA` update, `FRUTE` insert, `FJADWAL`.
- Delete data yang masih direferensikan FK → `1451` → pesan ramah (AC15).
- Insert nilai unik ganda → `1062` → pesan ramah.
- `tersedia` negatif → clamp 0 (sudah ada di `FPESANAN`).
- Waktu tanpa menit (mis. "7") → `CekWaktu` menolak; harus "7:00"/"07:00".
- DB mati saat form terbuka → `crud` melempar `MySqlException` → ditangkap form → pesan koneksi + log, grid tetap kosong, aplikasi hidup (AC9).
- Double-fire handler (FPETUGAS search) → kedua handler memanggil `CariData()` yang sama (idempoten) → tidak crash, tidak duplikasi (FR6.5).
- `FGERBONG` dibuka langsung (mis. via reflection) → hanya grid kosong, tidak crash; tidak ada CRUD yang bisa dieksekusi.

**Skema DB — catatan konfirmasi**: Tidak ada file `.sql`. Struktur disimpulkan dari query yang ada (Asumsi 1). Sebelum mengubah query, **verifikasi kolom langsung ke database** (`SHOW COLUMNS FROM <tabel>`) saat implementasi. Bila ada perbedaan nama kolom, sesuaikan parameter query — tidak mengubah skema. Hal ini dijalankan di fase implementasi, bukan desain.

## 12. Testability

- **Unit-testable (murni)**: `Validasi.HarusIsi/CekInteger/CekWaktu`; `db.AppLog`; helper `db.Fill/Scalar/ExecNonQuery` dengan koneksi (bila dibuat proyek test dengan DB lokal/MySQL test container). Saat ini **tidak ada proyek test** di solusi; menambah proyek test opsional (NFR: "bila memungkinkan"). Tidak diblokir, tetapi tidak diwajibkan.
- **Integration-test (manual, butuh DB)**: seluruh alur CRUD form. Karena membutuhkan MySQL `dbkereta_api` aktif, pengujian utama bersifat manual sesuai 12 skenario AC.
- Form yang sulit diuji otomatis (UI+DB) diuji manual per AC. Desain sudah menjaga agar logika DB terpisah di `db.cs` (testable) dan validasi terpisah di `Validasi` (testable) — pemisahan ini sengaja untuk ketertestabilan.

## 13. Rencana verifikasi (build + uji)

1. **Build**: `msbuild SitiKanisa_Tiket-Kereta\SitiKanisa_Tiket-Kereta.csproj /p:Configuration=Debug /restore`. Target net472; paket NuGet di `..\packages`. Build harus lolos tanpa error baru (AC17). Verifikasi tidak ada referensi ke field `db.koneksi/da/perintah` yang dihapus (sudah dipastikan via pencarian: nol).
2. **Restore** dilakukan MSBuild dari `packages.config` (pertahankan, bukan `PackageReference`).
3. **Uji manual per 12 skenario AC** (insert sukses/sekalig-tanpa-duplikat, insert kosong ditolak, update sukses per baris benar, update tanpa pilih baris, delete + batal delete, akses Admin, akses Super Admin, koneksi DB bermasalah, refresh grid, tanpa error berulang, input non-numerik ditolak, integritas relasi). Catat hasil di README bagian E.
4. Bersihkan artefak sementara jika ada.

## 14. README.md (di root workspace luar)

Isi mengikuti FR12 / bagian A–F:
- **A. Informasi project**: nama (SitiKanisa Tiket-Kereta), deskripsi, teknologi (C# WinForms .NET 4.7.2, Guna.UI2, Crystal Reports), database (MySQL `dbkereta_api`).
- **B. Ringkasan perubahan**: tabel file diubah + fungsi diperbaiki (CRUD, error, validasi, keamanan, hak akses, `db.cs`).
- **C. Alur aplikasi**: login (`Form1`) → `CekAkses`/`TerapkanAksesRole` di `MENU` → buka form via `KF.UntukForm` ke panel → CRUD → refresh grid. Sertakan matriks hak akses Admin/Super Admin.
- **D. Alur CRUD per form**: Insert/Read/Update/Delete berurutan untuk setiap form (parameterized, validasi, try-catch, refresh).
- **E. Hasil pengujian**: tabel 12 skenario + status (Lulus/Belum) + masalah tersisa (mis. `.rpt` tidak ada, `FGERBONG` belum terintegrasi, cetak per-baris `RIWAYAT` belum diimplementasikan).
- **F. Catatan teknis**: struktur DB (tabel+FK hasil inferensi), konfigurasi koneksi (`db.ConnectionString`, `DbUser`/`DbPassword`), dependensi (versi paket), letak log (`%LocalAppData%\...`), catatan build, batasan/out-of-scope.
README ditulis setelah implementasi agar "Hasil pengujian" jujur (AC16 mengharuskan bagian E ada; pengujian harus benar-benar dijalankan dulu — tidak boleh mengaku lulus sebelum diuji).

## 15. Hal yang sengaja TIDAK dikerjakan (Out of Scope, konsisten dengan requirements)

- Perubahan UI/layout/tema/struktur grid. Hanya `Validasi.cs` yang merupakan file baru (logika), bukan UI.
- Implementasi CRUD `FGERBONG` (stub; menunggu konfirmasi tabel).
- Pembuatan/pengeditan file `.rpt` Crystal Report; mengubah autentikasi/hashing password; mengubah kredensial DB (hanya mempusatkan).
- Migrasi framework/library; mengubah skema DB.
- Pengujian otomatis menyeluruh bila tidak ada kerangka; pengujian utama manual.

## 16. Respons terhadap review

Tidak ada `design-review.json` maupun `03-design-review.md` di run directory (`2026-11-03_14-49-10`) ini — hanya `01-requirements.md`. Oleh karena itu ini adalah desain awal, bukan revisi, dan tidak ada temuan review yang perlu ditanggapi. Semua keputusan diturunkan langsung dari `01-requirements.md` dan verifikasi kode sumber. Bila review berikutnya memberi temuan, setiap temuan akan ditanggapi pada bagian ini dengan pilihan: ditangani / masuk backlog (dengan alasan) / diabaikan dengan justifikasi — sejalan dengan FR asli.
