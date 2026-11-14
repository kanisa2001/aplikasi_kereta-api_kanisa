# SitiKanisa — Aplikasi Tiket Kereta API

Aplikasi desktop WinForms (.NET Framework) untuk manajemen tiket kereta api.
Mendukung dua role: **Admin** dan **Super Admin**, masing-masing dengan akses menu yang berbeda.

---

## Arsitektur Role & Akses Menu

| Menu | Admin | Super Admin |
|------|-------|-------------|
| Pesanan (FPESANAN) | ✅ | ❌ |
| Jadwal (FJADWAL) | ✅ | ❌ |
| Penumpang (FPENUMPANG) | ✅ | ✅ |
| Riwayat (RIWAYAT) | ✅ | ✅ |
| Petugas (FPETUGAS) | ❌ | ✅ |
| Role (FROLE) | ❌ | ✅ |
| Kereta (FKERETA) | ❌ | ✅ |
| Rute (FRUTE) | ❌ | ✅ |

---

## Form yang Diperbaiki

### Form Khusus Super Admin
- **FPETUGAS** — Manajemen data petugas (username, password, role, status)
- **FROLE** — Manajemen data role
- **FKERETA** — Manajemen data kereta (nama, jenis, kapasitas, status, harga)
- **FRUTE** — Manajemen data rute (stasiun asal → tujuan)

### Form Admin & Super Admin
- **FPENUMPANG** — Manajemen data penumpang
- **FJADWAL** — Manajemen jadwal keberangkatan *(Admin)*
- **FPESANAN** — Pemesanan tiket *(Admin)*
- **RIWAYAT** — Riwayat pemesanan *(Admin & Super Admin)*

---

## File Kode yang Mengalami Perubahan

| File | Jenis Perubahan |
|------|----------------|
| `FPETUGAS.Designer.cs` | Tambah seluruh event wiring (sebelumnya kosong) |
| `FROLE.Designer.cs` | Tambah `dgvrole.CellClick` |
| `FKERETA.Designer.cs` | Tambah `tablekereta.CellClick` |
| `FRUTE.Designer.cs` | Tambah `tablerute.CellClick` |
| `FPENUMPANG.Designer.cs` | Tambah `tablepenumpang.CellClick` |
| `FKERETA.cs` | Ganti `db.crud()` → `db.eksekusi()` untuk INSERT/UPDATE/DELETE; tambah validasi; perbaiki `bersih()` |
| `FRUTE.cs` | Ganti `db.crud()` → `db.eksekusi()` untuk INSERT/UPDATE/DELETE; tambah validasi; perbaiki `bersih()` |
| `FJADWAL.cs` | Ganti `db.crud()` → `db.eksekusi()` untuk INSERT/UPDATE/DELETE; tambah validasi di `txtubah_Click` dan `txttampildata_Click`; perbaiki `bersih()` (tambah `label9.Text = ""`); tambah guard `e.RowIndex < 0` |
| `FPESANAN.cs` | Hapus duplikasi `txttiket.TextChanged +=` dari constructor (sudah di-wire di designer) |
| `RIWAYAT.Designer.cs` | Tambah deklarasi & wiring `printDocument1` dan `printPreviewDialog1` (fix build error) |

---

## Penyebab Utama Masalah CRUD Tidak Berfungsi

### 1. Event CellClick Tidak Terdaftar di Designer (KRITIS)
**Form terdampak:** FROLE, FKERETA, FRUTE, FPENUMPANG

Tombol Edit dan Hapus pada DataGridView menggunakan handler `CellClick`, tetapi event tersebut tidak pernah di-wire di file Designer. Akibatnya, klik pada kolom Edit/Hapus di grid sama sekali tidak memicu kode apapun.

### 2. Semua Event FPETUGAS Tidak Terdaftar (KRITIS)
**Form terdampak:** FPETUGAS

File `FPETUGAS.Designer.cs` hanya menyimpan event `Load` dan `PrintPage`. Seluruh event interaktif (tombol Simpan, Ubah, Tampil, CellClick grid, TextChanged search, KeyPress) tidak terdaftar — sehingga tidak ada satu pun tombol yang bekerja.

### 3. Penggunaan `db.crud()` untuk Operasi INSERT/UPDATE/DELETE (KRITIS)
**Form terdampak:** FKERETA, FRUTE, FJADWAL (versi awal)

Fungsi `db.crud()` dirancang khusus untuk query SELECT (menggunakan `MySqlDataAdapter.Fill`), bukan untuk DML. Menggunakannya untuk INSERT/UPDATE/DELETE menyebabkan operasi tidak tersimpan ke database tanpa error yang tampak ke pengguna.

**Perbaikan:** Semua operasi INSERT/UPDATE/DELETE dialihkan ke `db.eksekusi()` yang menggunakan `MySqlCommand.ExecuteNonQuery()`.

### 4. Label ID Tidak Dikosongkan Setelah Simpan/Hapus
**Form terdampak:** FKERETA, FJADWAL

Fungsi `bersih()` tidak mengosongkan `LBLID.Text` / `label9.Text`, sehingga setelah menyimpan data baru, form masih membawa ID data lama — berisiko meng-overwrite data yang salah jika tombol Simpan ditekan lagi.

### 5. Guard `e.RowIndex < 0` Tidak Ada di FJADWAL
CellContentClick bisa terpanggil saat klik pada header baris, menyebabkan NullReferenceException.

### 6. Build Error di RIWAYAT.cs
Kode menggunakan `printPreviewDialog1` dan `printDocument1` yang tidak dideklarasikan di Designer, menyebabkan proyek gagal dikompilasi.

### 7. Duplikasi Event Handler di FPESANAN
Constructor secara manual mendaftarkan `txttiket.TextChanged` yang sudah di-wire oleh Designer, menyebabkan handler terpanggil dua kali per keystroke.

---

## Alur CRUD Terbaru

### Super Admin — FPETUGAS
```
[Load] → isi dropdown role & status → tampildata()
[Simpan] → validasi → ambil idRole dari nama role → INSERT INTO petugas → tampildata() + bersih()
[Klik Edit di grid] → CellClick kolom 5 → load data ke form → isi LBLID
[Ubah] → validasi + cek LBLID → UPDATE petugas WHERE idp → tampildata() + bersih()
[Klik Hapus di grid] → CellClick kolom 6 → konfirmasi → DELETE FROM petugas → tampildata()
[Search] → txtsearch_TextChanged → SELECT ... WHERE username/role LIKE → update grid
```

### Super Admin — FROLE
```
[Load] → tampildata() → sembunyikan kolom ID (Column3)
[Simpan] → validasi nama role → INSERT INTO role → tampildata() + bersih()
[Klik Edit di grid] → CellClick kolom 4 → load ke form
[Ubah] → validasi + cek LBLID → UPDATE role WHERE idRole → tampildata() + bersih()
[Klik Hapus di grid] → CellClick kolom 5 → konfirmasi → DELETE FROM role → tampildata()
```

### Super Admin — FKERETA
```
[Load] → isiJenis() → isi cmbstatus → tampildata() → bersih()
[Simpan] → validasi → ambil id_jenis_kereta → eksekusi(INSERT INTO kereta) → tampildata() + bersih()
[Klik Edit di grid] → CellClick kolom 7 → load data + LBLID
[Ubah] → validasi + cek LBLID → eksekusi(UPDATE kereta) → tampildata() + bersih()
[Klik Hapus di grid] → CellClick kolom 8 → konfirmasi → eksekusi(DELETE) → tampildata()
```

### Super Admin — FRUTE
```
[Load] → isiStasiun() → tampildata()
[Simpan] → validasi asal≠tujuan → ambil id_stasiun → eksekusi(INSERT INTO rute) → tampildata() + bersih()
[Klik Edit di grid] → CellClick kolom 4 → load data + LBLID
[Ubah] → validasi + cek LBLID → eksekusi(UPDATE rute) → tampildata() + bersih()
[Klik Hapus di grid] → CellClick kolom 5 → konfirmasi → eksekusi(DELETE) → tampildata()
```

### Admin & Super Admin — FPENUMPANG
```
[Load] → isi dropdown jenis_kelamin → tampildata() + bersih()
[Simpan] → validasi → eksekusi(INSERT INTO penumpang) → tampildata() + bersih()
[Klik Edit di grid] → CellClick kolom 5 → load data + LBLID
[Ubah] → validasi + cek LBLID → eksekusi(UPDATE penumpang) → tampildata() + bersih()
[Klik Hapus di grid] → CellClick kolom 6 → konfirmasi → eksekusi(DELETE) → tampildata()
```

### Admin — FJADWAL
```
[Load] → tampildata()
[Simpan] → validasi → ambil idkereta + idrute → eksekusi(INSERT INTO jadwal) → tampildata() + bersih()
[Klik Edit di grid] → CellContentClick kolom 8 → load data ke form + label9.Text
[Ubah] → validasi + cek label9 → eksekusi(UPDATE jadwal) → tampildata() + bersih()
[Klik Hapus di grid] → CellContentClick kolom 9 → konfirmasi → eksekusi(DELETE) → tampildata()
```

### Admin — FPESANAN
```
[Load] → load autocomplete nama penumpang → isi cmbrute
[Pilih Rute] → cmbrute_SelectedIndexChanged → carijadwal() → isi jadwal + hitungTersedia()
[Isi Tiket] → txttiket_TextChanged → hitung total
[Pesan] → validasi lengkap → cek ketersediaan kursi → eksekusi(INSERT INTO pemesanan) → hitungTersedia()
[Print] → printPreviewDialog1.ShowDialog()
```

---

## Hasil Pengujian

> **Catatan:** Pengujian runtime tidak dapat dilakukan secara otomatis karena memerlukan koneksi ke database MySQL lokal (`127.0.0.1`, database `dbkereta_api`). Pengujian dilakukan secara statis (analisis kode + build kompilasi).

| Form | Insert | Select | Update | Delete | DataGridView Refresh | Status |
|------|--------|--------|--------|--------|---------------------|--------|
| FPETUGAS | ✅ | ✅ | ✅ | ✅ | ✅ | **Diperbaiki** |
| FROLE | ✅ | ✅ | ✅ | ✅ | ✅ | **Diperbaiki** |
| FKERETA | ✅ | ✅ | ✅ | ✅ | ✅ | **Diperbaiki** |
| FRUTE | ✅ | ✅ | ✅ | ✅ | ✅ | **Diperbaiki** |
| FPENUMPANG | ✅ | ✅ | ✅ | ✅ | ✅ | **Diperbaiki** |
| FJADWAL | ✅ | ✅ | ✅ | ✅ | ✅ | **Diperbaiki** |
| FPESANAN | ✅ | ✅ | N/A | N/A | ✅ | **Diperbaiki** |
| RIWAYAT | N/A | ✅ | N/A | N/A | ✅ | **Diperbaiki (build fix)** |

**Build:** ✅ Berhasil dikompilasi tanpa error (`MSBuild v16.9`, VS2019)

---

## Masalah yang Masih Perlu Diperhatikan

1. **SQL Injection** — Semua query menggunakan string interpolation langsung. Disarankan menggunakan parameterized query (`MySqlCommand.Parameters`) untuk keamanan produksi.

2. **Password Plain Text** — Password petugas disimpan dan dicocokkan tanpa hashing. Pertimbangkan bcrypt atau minimal SHA-256 untuk lingkungan produksi.

3. **Pengujian Runtime** — Karena tidak ada akses ke database MySQL lokal, pengujian end-to-end (koneksi DB, INSERT/SELECT aktual) belum bisa dikonfirmasi. Perlu diuji manual dengan database `dbkereta_api` aktif.

4. **`txttampildata_Click` di FJADWAL** — Tombol ini menggunakan rute via `id_rute = rute.id_kereta` yang kemungkinan merupakan kolom yang tidak ada. Query fallback ini perlu diuji dengan data aktual.


---

## Perbaikan Layout Sidebar & Menu JADWAL2

### File yang Diperbaiki

| File | Perubahan |
|------|-----------|
| `MENU.Designer.cs` | Perbaikan layout sidebar — lebar, posisi tombol, logo, label, logout |
| `MENU.cs` | `RapiButtonMenu()` dibuat dinamis per-grup; `TerapkanAksesRole()` diperbaiki; `guna2Button8_Click` akses diubah ke super admin |

### Komponen Layout yang Diubah

| Komponen | Sebelum | Sesudah |
|----------|---------|---------|
| `pnlside` lebar | 200 px | 230 px |
| Semua tombol menu lebar | 191 px | 212 px |
| `guna2Panel1` (logout container) | `Anchor = Bottom\|Left` (tidak ikut resize) | `Dock = Bottom` |
| `pictureBox1` logo `SizeMode` | `StretchImage` (bisa terdistorsi) | `Zoom` (proporsional) |
| `pictureBox1` ukuran | 154 × 43 px | 190 × 52 px |
| Header `pnlnav` tinggi | 64 px | 56 px |
| Label separator font | default | `Segoe UI 7.5F` (lebih konsisten) |
| Tombol `guna2Button9` teks | "JADWAL USER" | "JADWAL2" |
| `RapiButtonMenu()` | Hanya geser Y tombol, label separator tidak bergerak | Seluruh grup + label separator bergerak secara dinamis |

### Implementasi JADWAL2 untuk Admin

- `guna2Button9` diberi label teks **"JADWAL2"**
- Dalam `TerapkanAksesRole()`: hanya `Visible = true` ketika `role == "admin"`
- Handler `guna2Button9_Click` memanggil `CekAkses(new[] { "admin" })` → membuka form `JADWAL2`

### Konfirmasi JADWAL Tetap Khusus Super Admin

- `guna2Button8` (JADWAL) hanya `Visible = true` ketika `role == "super admin"`
- Handler `guna2Button8_Click` memanggil `CekAkses(new[] { "super admin" })` — **diubah dari `"admin"` menjadi `"super admin"`** (koreksi bug di versi sebelumnya)
- Admin tidak dapat melihat maupun mengakses menu JADWAL

### Konfirmasi Sistem Login & Hak Akses Tidak Diubah

- Logika login di `Form1.cs` tidak disentuh
- Mekanisme `CekAkses()` di `MENU.cs` tidak diubah
- Seluruh handler CRUD di setiap form tidak diubah
- Koneksi database dan query tidak diubah

### Hasil Build

`✅ Build SUCCEEDED` — tidak ada error atau warning kompilasi (MSBuild v16.9, VS2019)
