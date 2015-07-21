using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Commons{

    public class EnabledButtons{
        public List<TextBox> texts = new List<TextBox>();
        public List<Button> buttons = new List<Button>();
        
        public void RegisterTextBox(TextBox tb){
            tb.TextChanged += (s, e) => this.ValidateMethod();
            this.texts.Add(tb);
        }

        public void RegisterButton(Button button){
            button.Enabled = false;
            this.buttons.Add(button);
        }
        
        public void ValidateMethod(){
            foreach (var t in this.texts)
                if (string.IsNullOrEmpty(t.Text)){
                    foreach (var b in this.buttons) b.Enabled = false;
                    return;
                }
            foreach (var b in this.buttons) b.Enabled = true;
        }
    }
}
