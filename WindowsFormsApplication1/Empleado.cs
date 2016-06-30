using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Empleado
    {
        //-	cédula 
        //-	nombre 
        //-	apellidos
        //-	edad 
        //-	fecha ingreso
        //-	oficina (rrhh, proveduria, compras, mercadeo)
        //-	salario base
        //-	salario
        public int cedula { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string edad { get; set; }
        //Se evita usar DateTime para evitar problemas con Trycatch de momento
        //public DateTime fechaingreso { get; set; }
        public int dia { get; set; }
        public int mes { get; set; }
        public int anno { get; set; }
        public string oficina { get; set; }
        public double salariob { get; set; }
        public double salario { get; set; }

        public Empleado() { }

        public Empleado(
            string n, string a, string ed, int d, int m , int an, string ofic, double sal ,double salariofinal) {
                nombre = n;
                apellido = a;
                edad = ed;
                dia = d;
                mes = m;
                anno = anno;
                oficina = ofic;
                salariob = sal;
                salario = salariofinal;
        }


        public void calcularSalario() {
            double calc = salariob;
            double deducciones = 0;
            double incentivos = 0;
            double asociacion = 0;
            double departamento = 0;
            //seguro
            deducciones = (calc * 0.13);
            //segun depto
            if (oficina.Equals("proveduría"))
            {
                departamento = (calc * 0.10); 
            }
            if (oficina.Equals("compras"))
            {
                departamento = (calc * 0.05);
            }
            if (oficina.Equals("mercadeo"))
            {
                departamento = (calc * 0.15);
            }
            //Asociacion
            asociacion = calc * 0.05;
            //incentivos
            if (salariob>= 500000)
            {
                incentivos = calc * 0.05;
            }
            //antiguedad
            DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime a = new DateTime(anno, mes, dia);
            //anno acutal 
            DateTime rightnow = DateTime.Now;
            DateTime b = new DateTime(rightnow.Year, rightnow.Month, rightnow.Day);
            TimeSpan span = b - a;
            // because we start at year 1 for the Gregorian 
            // calendar, we must subtract a year here.
            int years = (zeroTime + span).Year - 1;
            Console.WriteLine(years);
            //Si tiene mas de 10 años de laborar 
            if (years >= 10)
            {
                incentivos += calc * 0.10;
            }
            salario = calc-deducciones-asociacion+incentivos+departamento;                
        }
    }
}
