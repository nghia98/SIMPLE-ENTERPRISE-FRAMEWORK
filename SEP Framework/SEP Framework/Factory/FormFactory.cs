using SEP_Framework.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP_Framework.Factory
{
    public enum FormType
    {
        CREATE,
        READ,
        UPDATE,
        DELETE,
    };

    public class FormFactory
    {
        public static FormRoot getForm(FormType formType, string connectionString, string strNameTable)
        {
            switch (formType)
            {
                case FormType.CREATE:

                    return new CreateForm(connectionString, strNameTable);

                case FormType.READ:

                    return new ReadForm(connectionString, strNameTable);

                case FormType.UPDATE:

                    return new UpdateForm(connectionString, strNameTable);

                case FormType.DELETE:

                    return new DeleteForm(connectionString, strNameTable);

                default:
                    throw new Exception("Type Form không phù hợp !");
                    
            }
        }
    }
}
