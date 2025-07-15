namespace KnowCloud.Utility
{
    public class Utilities
    {
        public static string AuthAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public static string OrderAPIBAse { get; set; }
        public static string CouponAPIBase { get; set; }

        public const string TokenCookie = "JWTToken";
        public const string RoleAdmin = "ADMINISTRADOR";
        public const string RoleCustomer = "CUSTOMER";

        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE,
        }

        public enum ContentType
        {
            Json,
            MultipartFormData
        }

        public const string Status_pending = "Pendiente";
        public const string Status_Approved = "Aprovado";
        public const string Status_ReadyForPickup = "Listo para recoger";
        public const string Status_Completed = "Completo";
        public const string Status_Refunded = "Regresado";
        public const string Status_Canceled = "Cancelado";






    }
}
