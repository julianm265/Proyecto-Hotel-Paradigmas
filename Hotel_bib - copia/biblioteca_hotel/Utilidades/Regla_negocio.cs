namespace biblioteca_hotel.Utilidades
{
    public static class Regla_negocio
    {
        public static string regex_id = @"^\d{6,10}$";
        public static string regex_telefono = @"^\d{7,10}$";
        public static float valor_IVA = 0.19f;
        public static float seguro_hotelero = 0.02f;
    }
}
