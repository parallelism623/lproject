namespace lproject.Mediator.Mediator;

public static class Utils
{
    public static bool IsRequestType(Type t)
    {
        if (t.IsAbstract || t.IsInterface)
            return false;
        
        if (typeof(IRequest).IsAssignableFrom(t))
            return true;
        
        return t.GetInterfaces()
            .Any(i => i.IsGenericType &&
                      i.GetGenericTypeDefinition() == typeof(IRequest<>));
    }

    public static bool IsNotifyType(Type t)
    {
        if (t.IsAbstract || t.IsInterface)
            return false;

        return typeof(INotify).IsAssignableFrom(t);
    }

    public static bool IsRequestHandlerType(Type t)
    {
        if (t.IsAbstract || t.IsInterface)
            return false;

        return t.GetInterfaces().Any(i =>
            i.IsGenericType &&
            (i.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
             i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));
    }

    public static bool IsNotifyHandlerType(Type t)
    {
        if (t.IsAbstract || t.IsInterface)
            return false;

        return t.GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(INotifyHandler<>));
    }
    
    public static bool IsRequestHandlerInterface(Type itf)
    {
        if (!itf.IsInterface)
            return false;

        
        if (itf.IsGenericType)
        {
            var def = itf.GetGenericTypeDefinition();
            if (def == typeof(IRequestHandler<>) ||
                def == typeof(IRequestHandler<,>))
            {
                return true;
            }
        }

        
        foreach (var parent in itf.GetInterfaces())
        {
            if (parent.IsGenericType)
            {
                var def = parent.GetGenericTypeDefinition();
                if (def == typeof(IRequestHandler<>) ||
                    def == typeof(IRequestHandler<,>))
                {
                    return true;
                }
            }
        }

        return false;
    }

}