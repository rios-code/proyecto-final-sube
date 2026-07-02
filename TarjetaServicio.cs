using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sube
{
    internal class TarjetaServicio
    {
        private const string RUTA = "tarjetas.txt";

        public List<Tarjeta> LeerTodos()
        {
            List<Tarjeta> lista = new();
            if (!File.Exists(RUTA)) return lista;

            foreach (var linea in File.ReadAllLines(RUTA))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split('|');
                lista.Add(new Tarjeta(p[0], p[1], decimal.Parse(p[2]), int.Parse(p[3])));
            }
            return lista;
        }

        public bool Guardar(Tarjeta t)
        {
            var lista = LeerTodos();
            if (lista.Any(x => x.IdTarjeta == t.IdTarjeta)) return false;
            File.AppendAllText(RUTA, $"{t.IdTarjeta}|{t.Pasajero}|{t.Saldo}|{t.ViajesRealizados}\n");
            return true;
        }

        public Tarjeta? Buscar(string id)
        {
            return LeerTodos().FirstOrDefault(x => x.IdTarjeta == id);
        }

        public bool Actualizar(Tarjeta t)
        {
            var lista = LeerTodos();
            var index = lista.FindIndex(x => x.IdTarjeta == t.IdTarjeta);
            if (index == -1) return false;
            lista[index] = t;
            GuardarArchivo(lista);
            return true;
        }

        public bool Eliminar(string id)
        {
            var lista = LeerTodos();
            var item = lista.FirstOrDefault(x => x.IdTarjeta == id);
            if (item == null) return false;
            lista.Remove(item);
            GuardarArchivo(lista);
            return true;
        }

        public void MostrarEstadisticas()
        {
            var lista = LeerTodos();
            Console.WriteLine("\n===== ESTADISTICAS =====");
            Console.WriteLine($"Total tarjetas: {lista.Count}");
            Console.WriteLine($"Total viajes: {lista.Sum(x => x.ViajesRealizados)}");
            Console.WriteLine($"Saldo promedio: {(lista.Count > 0 ? lista.Average(x => x.Saldo) : 0):0.00}");
        }

        private void GuardarArchivo(List<Tarjeta> lista)
        {
            List<string> lineas = new();
            foreach (var t in lista)
                lineas.Add($"{t.IdTarjeta}|{t.Pasajero}|{t.Saldo}|{t.ViajesRealizados}");
            File.WriteAllLines(RUTA, lineas);
        }
    }
}
