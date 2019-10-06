using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mel.Domain.Entities
{
    public class statsModel
    {
        public ulong count_mutant_dna { get; set; }
        public ulong count_human_dna { get; set; }

        private double ratio;

        public double Ratio
        {
            get
            {
                if (count_human_dna<1)                
                    return 0;
                
                double Resull= count_mutant_dna / count_human_dna;
                return Resull;
            }
            
        }

    }
}
