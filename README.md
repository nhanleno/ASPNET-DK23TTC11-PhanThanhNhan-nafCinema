# 🎬 Website Bán Vé Xem Phim Trực Tuyến nafCinema

Đây là một website bán vé xem phim trực tuyến được xây dựng bằng ASP.NET Core MVC. Dự án hỗ trợ người dùng xem lịch chiếu phim, đặt vé trực tuyến, và quản lý rạp/phim/vé từ phía quản trị viên.

## 🚀 Tính năng nổi bật

- 📅 Xem lịch chiếu phim theo ngày, rạp
- 🎟️ Đặt vé, chọn ghế trực tuyến
- 🧾 Quản lý phim, suất chiếu, vé (Admin)
- 🔒 Đăng ký, đăng nhập và phân quyền người dùng
- 📊 Thống kê doanh thu (chưa làm)

## 🛠️ Công nghệ sử dụng

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core (EF Core)
- SQL Server
- Bootstrap 5 / HTML / CSS / JavaScript
- Identity cho phân quyền

## 📦 Hướng dẫn cài đặt

### Yêu cầu:
- Visual Studio 2022 trở lên
- .NET SDK 8.0 hoặc 9.0
- SQL Server Express / LocalDB

### Các bước cài đặt:

```bash
# Clone repository
git clone https://github.com/ten-ban/phim-online.git
cd phim-online

# Mở bằng Visual Studio -> Build Solution
# Tạo database bằng cách 
Update-Database
```

## ▶️ Cách chạy ứng dụng

1. Mở Visual Studio và chọn dự án chính.
2. Chạy bằng IIS Express hoặc `dotnet run`.
3. Truy cập tại `https://localhost:5001` (hoặc port bạn thiết lập).

## 🧑‍💻 Cấu trúc thư mục

```
/Controllers
/Models
/ModelViews
/Views
/Views/Shared
/wwwroot
appsettings.json
Startup.cs
Program.cs
```

## 👥 Tài khoản mẫu

**Admin:**
- Email: admin@gmail.com
- Mật khẩu: 123456

**User:**
- Email: user1@gmail.com
- Mật khẩu: 123456

## 📄 Giấy phép

Dự án phục vụ mục đích học tập và nghiên cứu. Không sử dụng cho mục đích thương mại khi chưa được cho phép.

## 👨‍💻 Người thực hiện

- Phan Thanh Nhàn (170123545)
