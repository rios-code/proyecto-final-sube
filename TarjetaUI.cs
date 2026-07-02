using System;

namespace Sube
{
    internal class TarjetaUI
    {
        private TarjetaServicio servicio = new();

        public void Menu()
        {
            int op;
            do
            {
                Console.WriteLine("\n===== SISTEMA SUBE =====");
                Console.WriteLine("1. Emitir tarjeta");
                Console.WriteLine("2. Ver saldo y estadisticas");
                Console.WriteLine("3. Registrar viaje");
                Console.WriteLine("4. Recargar saldo");
                Console.WriteLine("5. Eliminar tarjeta");
                Console.WriteLine("6. Salir");
                Console.Write("Opcion: ");

                int.TryParse(Console.ReadLine(), out op);

                switch (op)
                {
                    case 1: Emitir(); break;
                    case 2: Ver(); break;
                    case 3: Viaje(); break;
                    case 4: Recargar(); break;
                    case 5: Eliminar(); break;
                }
            } while (op != 6);
        }

        void Emitir()
        {
            string id = Leer("ID: ");
            string nombre = Leer("Pasajero: ");
            decimal saldo = LeerDecimal("Saldo inicial: ");

            var t = new Tarjeta(id, nombre, saldo);
            Console.WriteLine(servicio.Guardar(t) ? "Tarjeta creada." : "ID ya existe.");
        }

        void Ver()
        {
            string id = Leer("ID: ");
            var t = servicio.Buscar(id);
            if (t == null)
            {
                Console.WriteLine("No encontrada.");
                return;
            }
            Console.WriteLine(t);
            servicio.MostrarEstadisticas();
        }

        void Viaje()
        {
            string id = Leer("ID: ");
            var t = servicio.Buscar(id);
            if (t == null)
            {
                Console.WriteLine("No encontrada.");
                return;
            }
            if (t.PagarViaje())
            {
                servicio.Actualizar(t);
                Console.WriteLine($"Viaje OK. Saldo: {t.Saldo}");
            }
            else
                Console.WriteLine("Saldo insuficiente (-200 limite).");
        }

        void Recargar()
        {
            string id = Leer("ID: ");
            var t = servicio.Buscar(id);
            if (t == null)
            {
                Console.WriteLine("No encontrada.");
                return;
            }
            decimal monto = LeerDecimal("Monto: ");
            t.CargarSaldo(monto);
            servicio.Actualizar(t);
            Console.WriteLine("Recarga exitosa.");
        }

        void Eliminar()
        {
            string id = Leer("ID a eliminar: ");
            Console.WriteLine(servicio.Eliminar(id) ? "Eliminada correctamente." : "No existe.");
        }

        string Leer(string msg)
        {
            string v;
            do
            {
                Console.Write(msg);
                v = Console.ReadLine() ?? "";
            } while (string.IsNullOrWhiteSpace(v));
            return v;
        }

        decimal LeerDecimal(string msg)
        {
            decimal v;
            do
            {
                Console.Write(msg);
            } while (!decimal.TryParse(Console.ReadLine(), out v) || v <= 0);
            return v;
        }
    }
}
