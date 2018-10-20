using ExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalCode
{
    interface ILiquidacion
    {
        bool TieneAvisos { get; set; }
    }

    class Liquidacion: ILiquidacion
    {
        public bool TieneAvisos { get; set; }
    }

    class Predio
    {
        public double Avaluo { get; set; }
    }
    class Renglon
    {
        public double ValorLiquidado { get; set; }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            //Datos Iniciales
            ILiquidacion liquidacion = new Liquidacion { TieneAvisos = false };
            dynamic predio = new Predio { Avaluo = 2000000 };
            dynamic salarioMinimo = 700000;
            dynamic renglonLiquidacion = new Renglon { ValorLiquidado = 0 };

            List<Renglon> renglones = new List<Renglon>();
            renglones.Add(new Renglon());
            renglones.Add(new Renglon());

            Console.WriteLine("Antes");
            Console.WriteLine(renglones.Count);

            var tr = new TypeRegistry();
            tr.RegisterSymbol("Predio", predio);
            tr.RegisterSymbol("Liq", liquidacion);
            tr.RegisterSymbol("smmlv", salarioMinimo);
            tr.RegisterSymbol("RenglonLiquidacion", renglonLiquidacion);
            tr.RegisterSymbol("renglones", renglones);
            var formula = new StringBuilder();
            formula.Append("RenglonLiquidacion.ValorLiquidado = Liq.TieneAvisos?Predio.Avaluo * 0.0015:Predio.Avaluo * 0.0030;");
            formula.Append("renglones.Add(RenglonLiquidacion);");
            var formulaExpresion = new CompiledExpression { StringToParse = formula.ToString(), TypeRegistry = tr };
            formulaExpresion.ExpressionType = CompiledExpressionType.StatementList;
            var ff = formulaExpresion.Eval();
            

            Console.WriteLine(ff);
            Console.WriteLine("Después");
            Console.WriteLine(renglones.Count);
            Console.WriteLine("Tecla Para Continuar");
            Console.ReadKey();
            Ejecutar(liquidacion, predio, renglones, renglonLiquidacion);


            Console.ReadKey();

        }

        public static void Ejecutar(ILiquidacion liquidacion, Predio predio, List<Renglon> renglones, dynamic renglonLiquidacion)
        {
            Console.WriteLine("Antes");
            Console.WriteLine(renglones.Count);

            var tr = new TypeRegistry();
            tr.RegisterSymbol("Predio", predio);
            tr.RegisterSymbol("Liq", liquidacion);
            tr.RegisterSymbol("RenglonLiquidacion", renglonLiquidacion);
            tr.RegisterSymbol("renglones", renglones);
            var formula = new StringBuilder();
            formula.Append("RenglonLiquidacion.ValorLiquidado = Liq.TieneAvisos?Predio.Avaluo * 0.0015:Predio.Avaluo * 0.0030;");
            formula.Append("renglones.Add(RenglonLiquidacion);");
            var formulaExpresion = new CompiledExpression { StringToParse = formula.ToString(), TypeRegistry = tr };
            formulaExpresion.ExpressionType = CompiledExpressionType.StatementList;
            var ff = formulaExpresion.Eval();

            Console.WriteLine(ff);
            Console.WriteLine("Después");
            Console.WriteLine(renglones.Count);
        }

    }
}

