using MobvenSozluk.Repository.DTOs.RequestDTOs;
using System.Text.Json.Serialization;

namespace MobvenSozluk.Repository.DTOs.ResponseDTOs
{
    public class CustomResponseDto<T>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PagingResult PageDetail { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SortingResult SortDetail { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FilterResult FilterDetail { get; set; }

        public T Data { get; set; }

        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data };
        }
        public static CustomResponseDto<T> Success(int statusCode, T data,PagingResult pagedResult)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data ,PageDetail=pagedResult};
        }
        public static CustomResponseDto<T> Success(int statusCode, T data, PagingResult pagedResult, SortingResult sortingResult)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data, PageDetail = pagedResult, SortDetail = sortingResult };
        }
        public static CustomResponseDto<T> Success(int statusCode, T data, PagingResult pagedResult, SortingResult sortingResult,FilterResult filterResult)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data, PageDetail = pagedResult, SortDetail = sortingResult, FilterDetail = filterResult };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
