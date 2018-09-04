using System;

namespace ErrorHandlingBehaviorLibrary
{
    public interface IExceptionToFaultConverter
    {
        object ConvertExceptionToFaultDetail(Exception error);
    }
}
