using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public interface IElementService
    {
        public Element GetElementAndMutualism(string element);
		public string GetElementByBirthYear(int birthYear);
	}
}
