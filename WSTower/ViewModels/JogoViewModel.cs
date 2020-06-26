using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSTower.ViewModels
{
    public class JogoViewModel
    {
        public int Id { get; set; }
        public string SelecaoCasa { get; set; }
        public string SelecaoVisitante { get; set; }
        public int PlacarCasa { get; set; }
        public int PlacarVisitante { get; set; }
        public byte[] UniformeCasa { get; set; }
        public byte[] UniformeVisitante { get; set; }

    }
}
