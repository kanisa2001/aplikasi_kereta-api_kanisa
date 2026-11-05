# Requirements — Perbaikan & Optimalisasi Sistem CRUD Aplikasi Desktop Kereta Api (SitiKanisa_Tiket-Kereta)

## Ringkasan

Aplikasi desktop WinForms C# (.NET Framework 4.7.2) dengan MySQL (database `dbkereta_api`) untuk pemesanan tiket kereta api. Pekerjaan ini adalah **perbaikan dan optimalisasi**, bukan pembuatan ulang: memperbaiki logika CRUD pada Dashboard Admin dan Super Admin, menanganan error, keamanan query, manajemen koneksi database, penegakan hak akses, serta pembuatan dokumentasi `README.md`. UI, layout, tema, dan struktur project yang ada dipertahankan.

Saat ini seluruh form CRUD membangun SQL dengan **string interpolation langsung** (`db.crud($"... {var} ...")`) sehingga rentan SQL Injection; helper `db.cs` memakai satu koneksi `static` dan satu `DataSet static` bersama tanpa `try-catch` dan tanpa membuka/menutup koneksi secara eksplisit; banyak handler simpan/ubah tidak memvalidasi input; dan ada bug logika per baris (mis. `FPENUMPANG` mengambil data baris terakhir, bukan baris yang dipilih).

## Asumsi ( karena ambigu, akan diverifikasi saat desain )

1. **Struktur tabel disimpulkan dari query yang ada** di kode, karena tidak ditemukan file `.sql`/skema di repo. Tabel yang teridentifikasi: `petugas(idp, username, password, idrole, status)`, `role(idRole, nama_role, keterangan)`, `kereta(idkereta, nama_kereta, id_jenis_kereta, kapasitas, status, harga)`, `jenis_kereta(id_jenis_kereta, nama_jenis)`, `stasiun(id_stasiun, nama_stasiun)`, `rute(id_rute, id_stasiun_asal, id_stasiun_tujuan)`, `jadwal(idjadwal, id_kereta, id_rute, tanggal, jam_berangkat, jam_tiba, Harga)`, `penumpang(idpenumpang, nama, nik, jenis_kelamin, no_hp)`, `pemesanan(id_pemesanan, id_jadwal, nama_pemesan, no_identitas, no_telp, jumlah_tiket, metode_bayar, tanggal_pemesanan, harga, total_harga)`. **Skema riil harus dikonfirmasi terhadap database saat desain** sebelum mengubah query.
2. **Kebijakan hak akses dipertahankan persis** seperti yang sudah ada di `MENU.cs`: Admin → Pesanan, Jadwal, Penumpang, Riwayat; Super Admin → Petugas, Role, Kereta, Rute, Penumpang, Riwayat. Nama role di DB diasumsikan tepatnya `"admin"` dan `"super admin"` (lowercase setelah `Trim().ToLower()`).
3. **`README.md` diletakkan di root workspace** (`...\SitiKanisa_Tiket-Kereta\README.md`), sesuai frasa "direktori root workspace" pada permintaan. Jika yang dimaksud adalah root folder *project*, ini mudah disesuaikan.
4. **`FGERBONG` adalah stub kosong** (tidak ada CRUD, tidak ada tombol menu yang membukanya, dan tidak ada tabel `gerbong` yang dirujuk di mana pun). Ini diperlakukan sebagai **blokir yang perlu konfirmasi** (lihat Out of Scope). Asumsi default: jangan mengimplementasikan CRUD `FGERBONG` baru sebelum ada konfirmasi tabelnya ada; cukup dokumentasikan statusnya.
5. Nilai primary key di-generate database (auto-increment) untuk semua tabel yang menyertakan `VALUES (null, ...)`. Asumsi ini berlaku dan tidak diubah.
6. Kredensial database (root tanpa password) bersifat development. Pengubahan autentikasi/hashing password **di luar scope** kecuali diminta.

## Functional Requirements

### FR1 — Create (Insert)
- 1.1 Pada setiap form CRUD (`FPETUGAS`, `FROLE`, `FKERETA`, `FRUTE`, `FJADWAL`, `FPENUMPANG`, `FPESANAN`), data baru tersimpan ke DB dan **langsung tampil di DataGridView tanpa duplikasi baris** (refresh grid sesuai sumber data, bukan append manual yang menumpuk).
- 1.2 Setelah simpan berhasil, form input dibersihkan dan kursor/kembali ke kondisi siap input.
- 1.3 Untuk `FPESANAN`, insert pemesanan hanya boleh berhasil bila `jumlah_tiket` tidak melebihi sisa kapasitas jadwal terkait, dan perhitungan sisa kapasitas harus konsisten dengan `txttiket_TextChanged` (tidak boleh dua sumber kebenaran yang berbeda).

### FR2 — Read (Select / Tampil)
- 2.1 Saat form dibuka (`_Load`), DataGridView menampilkan data terbaru dari DB secara akurat, termasuk hasil JOIN (mis. `petugas`→`role.nama_role`, `kereta`→`jenis_kereta`, `rute`→`stasiun`, `jadwal`→`kereta`+`rute`+`stasiun`).
- 2.2 Setelah operasi insert/update/delete, grid langsung di-refresh ke data terbaru.
- 2.3 Pencarian (`txtsearch`) memfilter data sesuai input (bukan menampilkan seluruh data), dan hasil pencarian **tetap menyertakan kolom primary key yang disembunyikan** agar edit/delete tetap valid.
- 2.4 `RIWAYAT` menampilkan data pemesanan gabungan dan tetap read-only.

### FR3 — Update
- 3.1 Mekanisme: klik baris → ambil primary key dari kolom tersembunyi → isi form input dengan data baris tersebut → ubah → simpan. Update menargetkan baris yang dipilih **berdasarkan primary key yang benar**.
- 3.2 Jika tidak ada baris terpilih (LBLID kosong), tombol Ubah tidak melakukan update dan memberi pesan yang jelas; tidak boleh update seluruh baris atau baris keliru.
- 3.3 Perbaikan bug spesifik: `FPENUMPANG` edit saat ini menjalankan `SELECT * FROM penumpang` **tanpa `WHERE`** sehingga form terisi data baris terakhir, bukan baris yang diklik. Harus memilih baris yang benar.
- 3.4 Perbaikan bug spesifik: `FJADWAL` handler `txttampildata_Click`/lookup rute memakai kolom `rute.id_kereta` yang **tidak ada** di tabel `rute`. Query harus dikoreksi agar sesuai skema.

### FR4 — Delete
- 4.1 Delete berdasarkan primary key yang benar (diambil dari kolom tersembunyi baris terpilih).
- 4.2 Wajib ada **konfirmasi** (`YesNo`) sebelum hapus; bila dibatalkan, tidak ada data berubah dan grid tetap utuh.
- 4.3 Hapus memperhatikan relasi: bila data masih direferensikan tabel lain (FK), operasi ditolak dengan pesan jelas (tidak crash, tidak silent error). Kebijakan cascade/penolakan mengikuti constraint DB yang ada.
- 4.4 Setelah hapus (atau batal hapus), grid di-refresh dan form dibersihkan.

### FR5 — Validasi Input
- 5.1 Semua field wajib divalidasi sebelum simpan/ubah: field teks tidak boleh kosong; combo harus terpilih (bukan kosong).
- 5.2 Field numerik (`kapasitas`, `harga`, `jumlah_tiket`, `no_hp`/`nik` bila numerik) divalidasi tipe dan rentang (mis. tiket > 0, harga ≥ 0).
- 5.3 Field tanggal (`tanggal`) dan waktu (`jam_berangkat`, `jam_tiba`) divalidasi format/bentuk (`FJADWAL` saat ini menambah `":00"` manual ke input teks — perlu validasi format waktu).
- 5.4 Validasi gagal menampilkan pesan spesifik perubahan dan membatalkan simpan.
- 5.5 `FPESANAN` sudah memvalidasi kelengkapan; pola itu dijadikan standar minimal di seluruh form.

### FR6 — Penanganan Error & Logging
- 6.1 Setiap operasi DB (insert/update/delete/select) dibungkus `try-catch` yang menampilkan pesan **informatif** (bukan menelan error).
- 6.2 Silent error dihilangkan: event `DataError` DataGridView tidak boleh sekadar `ex.Cancel = true` tanpa umpan balik; root cause diperbaiki, bukan disembunyikan.
- 6.3 Kegagalan koneksi DB ditangani dengan pesan jelas dan aplikasi tidak keluar tiba-tiba.
- 6.4 Error dicatat (logging) agar dapat ditelusuri; mekanisme logging konsisten di seluruh form.
- 6.5 Mencegah eksekusi handler ganda yang menyebabkan CRUD berjalan dua kali (mis. `FPETUGAS` punya `btnsimpan_Click` kosong + `btnsimpan_Click_1` aktif — perlu dipastikan hanya satu handler aktif per tombol).

### FR7 — Manajemen Koneksi (`db.cs`)
- 7.1 Menghilangkan potensi **connection leak**: koneksi dibuka dan ditutup/dispose dengan benar di tiap operasi (pola open → use → close, idealnya `using`), tidak mengandalkan satu koneksi `static` yang persisten tanpa penutupan.
- 7.2 Menghindari kondisi race pada `DataSet`/`DataAdapter` static bersama ketika beberapa form operasi bersamaan (state bersama tidak boleh saling menimpa data antar form).
- 7.3 `crud()` tidak boleh melempar exception tak-tertangani yang mematikan aplikasi; kegagalan ditangani dan dilaporkan.
- 7.4 String koneksi terpusat (tidak diduplikasi; saat ini `db.cs` dan `Form2` sama-sama hardcode `root`/`""`) tanpa mengubah nilai kredensial yang sudah berjalan.

### FR8 — Keamanan Query (SQL Injection)
- 8.1 Seluruh query beralih dari string interpolation ke **parameterized query** (`MySqlParameter`) untuk semua nilai dari input pengguna, di semua form termasuk login (`Form1`) dan pencarian `LIKE`.
- 8.2 Nilai dari combo/list yang dimuat dari DB tetap diperlakukan sebagai input eksternal (di-parameterisasi).
- 8.3 Tidak ada lagi `db.crud($"... '{var}' ...")` dengan interpolasi langsung.

### FR9 — Hak Akses
- 9.1 Batasan akses diterapkan di **logika program** (`CekAkses`) sebelum membuka form, tidak hanya menyembunyikan tombol. Ini sudah ada; dipertahankan dan dibuat konsisten.
- 9.2 Konsistensi: bila role tidak dikenali, perilaku tombol-terlihat vs `CekAkses` harus selaras (saat ini fallback menampilkan semua tombol tapi `CekAkses` menolak — inkonsisten, perlu disamakan ke arah "tolak secara default").
- 9.3 Tidak ada fitur CRUD dapat diakses role yang tidak berwenang. Kebijakan akses tidak diubah.

### FR10 — Integritas & Transaksi
- 10.1 Primary key dan foreign key digunakan dengan benar pada semua query insert/update/delete.
- 10.2 Operasi multi-langkah yang harus atomik (mis. `FPESANAN`: cek kapasitas → insert pemesanan) dibungkus **transaksi** sehingga berhasil/gagal bersamaan.
- 10.3 Tidak ada data berubah/terhapus di luar proses yang dimaksud; relasi antartabel terjaga.

### FR11 — Perbaikan Bug Spesifik Teridentifikasi
- 11.1 `FPETUGAS`: handler `guna2TextBox1_TextChanged` mereferensikan kolom `idp` yang tidak ikut di-`SELECT`; `txtsearch_TextChanged` memuat ulang seluruh data, bukan memfilter. Duplikasi handler `btnsimpan` (kosong vs aktif) dirapikan.
- 11.2 `FROLE`: hasil pencarian `guna2TextBox1_TextChanged` menambah baris tanpa kolom id tersembunyi → edit/delete gagal. `btnubah_Click_1` tidak mengosongkan LBLID setelah update dan nama kolom `idrole` vs `idRole` dirapikan.
- 11.3 `FPENUMPANG`: bug pilih baris (FR3.3).
- 11.4 `FJADWAL`: kolom `rute.id_kereta` tidak valid (FR3.4); pastikan handler edit/delete terhubung ke grid yang benar (`tablejadwal`), bukan ke handler `guna2DataGridView1_CellContentClick_1` yang mungkin tidak terpasang.
- 11.5 `FPESANAN`: `txtnama_TextChanged` mengisi `txtidentitas` dengan `idpenumpang`, bukan `nik` — perlu dikonfirmasi field mana yang seharusnya ditampilkan/ digunakan.
- 11.6 `RIWAYAT`: kolom "Print" ada di grid tapi tidak ada handler baris untuk mencetak per baris; tombol `guna2Button1` membuka `Form2` tanpa memilih baris. Perilaku cetak dirapikan/didokumentasikan.

### FR12 — README.md
- 12.1 Dibuat di root workspace, berisi: (A) informasi project (nama, deskripsi, teknologi, database); (B) ringkasan perubahan (file diubah, fungsi diperbaiki); (C) alur aplikasi (login → cek role → dashboard → CRUD); (D) alur CRUD per form; (E) hasil pengujian; (F) catatan teknis (struktur DB, konfigurasi koneksi, dependensi).

## Non-Functional Requirements

- **Stabilitas:** Aplikasi tidak crash tiba-tiba akibat error DB/input; tidak ada silent error.
- **Keamanan:** Tidak ada SQL Injection; kredensial tidak diduplikasi di banyak tempat (tanpa mengubah nilai yang berjalan).
- **Keterbacaan:** Kode mudah dipahami, tidak bertele-tele, pola konsisten antar form (validasi, try-catch, refresh grid).
- **Kompatibilitas:** Tetap build pada .NET Framework 4.7.2 (Visual Studio / MSBuild); dependensi yang ada (Guna.UI2 2.0.4.8, MySql.Data 26.7.0, CrystalDecisions 13.0.4000) tidak ditambah/diganti.
- **Non-regresi:** Fitur yang sudah berjalan tidak rusak; UI/layout/tema/warna/struktur DataGridView tidak diubah kecuali benar-benar perlu.
- **Keterujuan:** Setiap perbaikan CRUD dapat diuji manual sesuai skenario; idealnya ada unit test untuk helper koneksi/query bila memungkinkan (opsional, mengikuti ekosistem proyek).
- **Mempertahankan struktur:** Penamaan file/ kontrol yang sudah ada dipertahankan selama masih layak; tidak membuat ulang sistem.

## Acceptance Criteria

1. **Insert sukses**: menambah data baru pada setiap form menyimpan ke DB dan baris baru muncul **sekali** di DataGridView (tidak duplikat, tidak menumpuk).
2. **Insert dengan input kosong/tidak valid**: aplikasi menolak simpan, menampilkan pesan validasi spesifik, dan tidak menyimpan data rusak ke DB.
3. **Update sukses**: memilih baris → field terisi data baris itu (bukan baris lain) → mengubah → menyimpan memperbarui baris yang benar di DB dan grid.
4. **Update tanpa pilih baris**: menekan Ubah tanpa memilih baris tidak mengubah data mana pun dan memberi pesan yang jelas.
5. **Delete sukses**: memilih baris → konfirmasi Yes → data terhapus dari DB dan grid berdasarkan primary key yang benar.
6. **Batal delete**: konfirmasi No → tidak ada data berubah, grid tetap utuh.
7. **Akses Admin**: hanya Pesanan, Jadwal, Penumpang, Riwayat yang bisa dibuka; upaya akses fitur lain (mis. Petugas) ditolak `CekAkses` dengan pesan, meski tombol disembunyikan/dilihat.
8. **Akses Super Admin**: Petugas, Role, Kereta, Rute, Penumpang, Riwayat dapat diakses; Pesanan/Jadwal ditolak.
9. **Koneksi DB bermasalah**: saat koneksi gagal, aplikasi menampilkan pesan informatif dan tidak keluar tiba-tiba; tidak ada koneksi bocor (dapat dibuka/tutup berulang tanpa error "connection already open").
10. **Refresh grid**: setelah insert/update/delete, DataGridView menampilkan data terbaru dari DB.
11. **Tanpa error berulang**: operasi CRUD diulang berkali-kali tidak memicu error ganda/ handler dobel.
12. **Input tidak sesuai**: input non-numerik pada field numerik (harga/kapasitas/tiket) ditolak dengan pesan, aplikasi tetap berjalan.
13. **SQL Injection tertutup**: seluruh query parameterized; tidak ada interpolasi nilai input langsung ke SQL (verifikasi via peninjauan kode).
14. **Bug spesifik hilang**: `FPENUMPANG` edit mengambil baris yang dipilih (AC3); `FJADWAL` tidak lagi memakai kolom `rute.id_kereta` tidak valid; `FROLE` pencarian menyertakan kolom id; `FPETUGAS` handler simpan tidak ganda dan pencarian memfilter.
15. **Integritas relasi**: menghapus data yang masih direferensikan ditolak dengan pesan jelas; pemesanan melebihi kapasitas ditolak; insert pemesanan atomik.
16. **README.md**: file ada di root workspace dan memuat keenam bagian (A–F) yang diminta, dengan alur login → role → dashboard → CRUD dijelaskan per form.
17. **Build sukses**: proyek ter-compile pada .NET Framework 4.7.2 tanpa error baru; fitur lama (login, cetak tiket/Crystal Report, autocomplete nama penumpang) tetap berfungsi.

## Out of Scope

- Perubahan UI/layout/tema/warna/struktur DataGridView yang tidak diperlukan untuk perbaikan logika.
- Penambahan fitur baru di luar perbaikan CRUD/error/keamanan yang diminta.
- Redesain skema database; perubahan tabel hanya minimal bila benar-benar diperlukan untuk konsistensi relasi, dan dikonfirmasi dulu.
- Implementasi CRUD `FGERBONG` baru: form ini **stub kosong, tidak terhubung ke menu, dan tidak ada tabel `gerbong` di kode**. Menunggu konfirmasi keberadaan tabel/kebutuhan fungsional sebelum dikerjakan; untuk sementara didokumentasikan sebagai belum terintegrasi.
- Pengubahan autentikasi/hashing password dan pengubahan kredensial DB (hanya mempusatkan konfigurasi, nilai tetap).
- Migrasi framework (mis. ke .NET Core/5+) atau penggantian library UI/ORM.
- Pengujian otomatis menyeluruh bila tidak ada kerangka pengujian; pengujian utama bersifat manual sesuai skenario di atas (implementasi boleh menambah unit test bila memungkinkan).
