using System;

namespace MISA.WebFresher052023.HCSN.Domain.Enum
{
    public enum ActionMode
    {
        /// <summary>
        /// Hành động thêm mới
        /// </summary>
        CREATE = 0,
        /// <summary>
        /// Hành động cập nhật
        /// </summary>
        UPDATE = 1,
        /// <summary>
        /// Hành động xóa
        /// </summary>
        DELETE = 2,
        /// <summary>
        /// không thay đổi
        /// </summary>
        UNCHANGE = 3
    }
}
