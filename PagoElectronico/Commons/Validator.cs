using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace PagoElectronico.Commons
{
    public class Validator{

        public Boolean blocked;

        public Validator(){
            this.blocked = false;
        }

        public Boolean validateInt(string texto){
            int output;
            return int.TryParse(texto, out output);
        }

        public Boolean validateDouble(string texto){
            double output;
            if (texto.ToCharArray().First() == (char)46 && !this.blocked) {
                this.blocked = true;
                return true;
            }
            return double.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out output);
        }

        public void KeyPressBinding(Func<string, Boolean> validatorFunc, Boolean space, KeyPressEventArgs e){
            if (!validatorFunc(e.KeyChar.ToString())) e.Handled = true;
            if (e.KeyChar == (char)8) e.Handled = false; //Permite Backspace
            if (e.KeyChar == (char)32) e.Handled = !space;
        }
    }
}
