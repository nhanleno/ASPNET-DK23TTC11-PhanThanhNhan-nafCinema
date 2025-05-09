using System.Collections.Generic;

namespace nafCine.ViewModels
{
    /// <summary>
    /// ViewModel tổng hợp để hiển thị giao diện chọn ghế cho một suất chiếu.
    /// Chứa thông tin về số hàng, số cột và danh sách các ghế (đã chuyển sang SeatViewModel).
    /// </summary>
    public class SeatSelectionViewModel
    {
        // Số hàng ghế của phòng chiếu (theo cấu hình layout)
        public int Rows { get; set; }

        // Số cột ghế, dựa trên tổng số ghế chia đều cho số hàng
        public int Columns { get; set; }

        // Danh sách các ghế hiển thị trên giao diện (đã chuyển đổi sang SeatViewModel)
        public List<SeatViewModel> Seats { get; set; } = new List<SeatViewModel>();

        // Mã của suất chiếu, để định danh phiên làm việc đặt vé này
        public int ShowtimeId { get; set; }
    }
}