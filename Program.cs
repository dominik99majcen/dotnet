/* Program za izpis ure na 16x2 LCD zaslon preko I2C komunikacije 
   Dominik Majcen
******************************************************************/
// Uporabljenje knjiznice
using System;
using System.Threading;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;
using System.Device.Gpio;
using System.Device.I2c;
//****************************************************************
namespace Lcd.Program
{
   class Izpis_na_Lcd
   {
      static void Main()
      {
         Console.WriteLine("Izpis trenutnega datuma in casa.");

         // Inicializacija LCD zaslona
         var i2c = I2cDevice.Create(new I2cConnectionSettings(busId: 1,deviceAddress: 0x27));
         var krmilnik = new Pcf8574(i2c);
         var lcd = new Lcd1602(dataPins: new int[] { 4, 5, 6, 7 },
                        registerSelectPin: 0,
                        readWritePin: 1,
                        enablePin: 2,
                        backlightPin: 3,
                        backlightBrightness: 0.1f,
                        controller: new GpioController(PinNumberingScheme.Logical, krmilnik));
         
         do
         {
            lcd.Clear();
            lcd.SetCursorPosition(0,0);
            lcd.Write(DateTime.Now.ToString("dd MMMMM yyyy")); // izpis datuma
            lcd.SetCursorPosition(0,1); 
            lcd.Write(DateTime.Now.ToString("HH:mm:ss")); // izpis ure
            Thread.Sleep(1000); // osvezitev prikaza vsako sekundo
         } while (true);
      }
   }
}