namespace biblioteca_hotel.Interfaces
{
    public interface IProceso
    {
        Modelos.Core.Factura Check_Out(Modelos.Personas.Persona persona);
        void Check_In(Modelos.Personas.Persona persona);
    }
}
