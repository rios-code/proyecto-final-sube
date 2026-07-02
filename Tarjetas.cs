using System;

namespace Sube
{
    internal class Tarjeta
    {
        public string IdTarjeta { get; set; }
        public string Pasajero { get; set; }
        public decimal Saldo { get; set; }
        public int ViajesRealizados { get; set; }

        public Tarjeta(string id, string pasajero, decimal saldo, int viajes = 0)
        {
            IdTarjeta = id;
            Pasajero = pasajero;
            Saldo = saldo;
            ViajesRealizados = viajes;
        }

        public bool PagarViaje()
        {
            if (Saldo - 100 < -200) return false;
            Saldo -= 100;
            ViajesRealizados++;
            return true;
        }

        public void CargarSaldo(decimal monto)
        {
            if (monto > 0) Saldo += monto;
        }

        public override string ToString()
        {
            return $"ID: {IdTarjeta} | Pasajero: {Pasajero} | Saldo: ${Saldo} | Viajes: {ViajesRealizados}";
        }
    }
}
