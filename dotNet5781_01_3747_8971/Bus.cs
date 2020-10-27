using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3747_8971
{
    class Bus
    {
        int[] licensePlate;
        DateTime dateActivity;
        float kilometers;
        bool status;
        float totalMiles;// נסיעה ק"מ כוללת 
        public static void Mileage(float _kh)//שמירה הנסיעה הכוללת 
        {
           this.totalMiles += _kh;

        }
        public bool FuelCondition()
        {
            Bus MyBus = new Bus();
            if (MyBus.kilometers > 1200)
                return true;
            return false;
        }
        public int NumberOflicensePlate()
        {
            Bus MyBus = new Bus();
            int year = int.Parse(MyBus.dateActivity.Year.ToString());
            return year < 2018 ? 7 : 8;
        }
        public bool TreatmentIsNeeded()
        {
          
            if (MyBus.kilometers>20000 || 
        }
        bool dateCheck()
        {
            Bus MyBus = new Bus();
            int day = int.Parse(MyBus.dateActivity.Day.ToString());
            int month = int.Parse(MyBus.dateActivity.Month.ToString());
            int year = int.Parse(MyBus.dateActivity.Year.ToString());
        }

    }
}

/* כל אוטובוס בבעלותה מאופיין במספר רישו
 * י. כמו כן צריך לדעת את תאריך תחילת הפעילות
של כלי התחבורה.
כדי לשמור על מצב כלי תחבורה תקין יש לבצע טיפול כללי מדי 000,20 ק"מ נסועה, או מדי
שנה, לפי המוקדם בינהם, יש לשים לב שע"פ התקנות אוטובוס שנסע יותר מ000,20 ק"מ
מהטיפול האחרון מוגדר מסוכן, ולכן אין לאפשר לאוטובוס לבצע נסיעה בה הוא יעבור את
מספר הק"מ המותר.
יש לשמור את הנסועה הכוללת )קילומטראז '( של כלי התחבורה.
בנוסף יש לשמור מידע על מצב הדלק בכלי התחבורה, אוטובוס יכול לנסוע לכל היותר 1200
ק"מ אחרי תדלוק, ולכן אין לאפשר לו לבצע נסיעה בה הוא יעבור את מספר הק"מ הזה ללא
תדלוק קודם.
הערות:
• מספר רישוי הוא מספר בן 7 לאוטובוסים שנכנסו לפעילות לפני שנת 2018 ו8 ספרות לשאר.
דרך ההצגה של מספר רישוי תתבצע בהתאמה לפי הפורמט הבא: 67 - 345 - 12 או 678 - 45 - 123
• אין לאפשר הפחתה של הנסועה הכוללת של כלי תחבורה*/

