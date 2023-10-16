using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE.ErrorViews.DecoratorPattern
{
    public interface IErrorsComponent
    {
        public  string GetErrors();
    }
    public class ErrorsComponent : IErrorsComponent
    {
        public  string GetErrors() {
            return "Errors\n";
        }
    }
    public abstract class ErrorDecorator : IErrorsComponent
    {
        protected IErrorsComponent component;
        public ErrorDecorator(IErrorsComponent component) {
            this.component = component;
        }
        public virtual string GetErrors()
        {
            return component.GetErrors();
        }
    }
    public class LexErrorDec : ErrorDecorator
    {
        public LexErrorDec(IErrorsComponent component) : base(component)
        {
        }
        
         public override string GetErrors()
        {
            string errors = File.ReadAllText(@"Archivo_Errores.txt");
            return base.GetErrors() + "Lexical Errors\n" +errors + "\n";
        }
    
    }
    public class SintaxErrorDec : ErrorDecorator
    {
        public SintaxErrorDec(IErrorsComponent component) : base(component)
        {
        }

         public override string GetErrors()
        {
            string errors = File.ReadAllText(@"Errores_Sintaxis.txt");
            return base.GetErrors() + "Sintax Errors\n" +errors + "\n";
        }
    }
    public class SemErrorDec : ErrorDecorator
    {
        public SemErrorDec(IErrorsComponent component) : base(component)
        {
        }

         public override string GetErrors()
        {
            string errors = File.ReadAllText(@"Archivo_ErrorSem.txt");
            Debug.WriteLine(base.GetErrors());
            return base.GetErrors() + "Semantic Errors\n" +errors + "\n";
        }
    }
}
