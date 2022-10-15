using Microsoft.AspNetCore.Mvc;

namespace Library.Infra.ResponseNotifier;

public interface IResponseFormatter
{
    ActionResult Format();
    ActionResult Format<T>(T body = null) where T : class;
}
