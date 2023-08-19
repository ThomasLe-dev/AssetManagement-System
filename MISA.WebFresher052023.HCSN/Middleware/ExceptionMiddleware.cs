using MISA.WebFresher052023.HCSN.Domain;

namespace MISA.WebFresher052023.HCSN
{
    /// <summary>
    /// Middleware xử lý các ngoại lệ trong ứng dụng
    /// </summary>
    public class ExceptionMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo một instance mới của ExceptionMiddleware
        /// </summary>
        /// <param name="next">Delegate tiếp theo trong pipeline</param>
        /// Created by: LB.Thành (16/07/2023)
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        /// <summary>
        /// Phương thức Invoke xử lý ngoại lệ
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// Created by: LB.Thành (16/07/2023)
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Phương thức xử lý ngoại lệ và trả về response
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="exception">Ngoại lệ</param>
        /// Created by: LB.Thành (16/07/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.Response.ContentType = "application/json";

            if (exception is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = ((NotFoundException)exception).Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
            else if(exception is ConflictException)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((ConflictException)exception).ErrorCode,
                        UserMessage = ((ConflictException)exception).Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = "Lỗi server",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
        }
    }
}
