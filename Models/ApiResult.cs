namespace NetBlocks.Models;

public class ApiResult : ResultBase<ApiResult>
{ }

public class ApiResultDto : ResultBase<ApiResult>.ResultDtoBase<ApiResult, ApiResultDto>
{
    public ApiResultDto() : base() { }

    public ApiResultDto(ApiResult? result) : base(result) { }
}


public class ApiResult<T> : ResultContainerBase<ApiResult<T>, T>
{
    public ApiResult() : base() { }

    public ApiResult(T value) : base(value) { }
}

public class ApiResultDto<TDto> : ResultBase<ApiResult<TDto>>.ResultDtoBase<ApiResult<TDto>, ApiResultDto<TDto>>
{
    public TDto? Value { get; set; }

    public ApiResultDto() : base() { }

    public ApiResultDto(ApiResult<TDto>? result) : base(result)
    {
        if (result != null) Value = result.Value;
    }

    public override ApiResult<TDto> From()
    {
        var result = base.From();
        result.Value = Value;
        return result;
    }
}