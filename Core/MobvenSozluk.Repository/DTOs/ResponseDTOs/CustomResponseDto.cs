using MobvenSozluk.Repository.DTOs.RequestDTOs;
using System.Text.Json.Serialization;

namespace MobvenSozluk.Repository.DTOs.ResponseDTOs
{
    public class CustomResponseDto<T>
    {
        public PagingResult PageDetail { get; set; }
        public SortingResult SortDetail { get; set; }
        public FilterResult FilterResult { get; set; }

        public T Data { get; set; }

        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data)// static factory method design pattern
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data };
        }
        public static CustomResponseDto<T> Success(int statusCode, T data,PagingResult pagedResult)// static factory method design pattern
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data ,PageDetail=pagedResult};
        }
        public static CustomResponseDto<T> Success(int statusCode, T data, PagingResult pagedResult, SortingResult sortingResult)// static factory method design pattern
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data, PageDetail = pagedResult, SortDetail = sortingResult };
        }
        public static CustomResponseDto<T> Success(int statusCode, T data, PagingResult pagedResult, SortingResult sortingResult,FilterResult filterResult)// static factory method design pattern
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data, PageDetail = pagedResult, SortDetail = sortingResult, FilterResult = filterResult };
        }
        public static CustomResponseDto<T> Success(int statusCode) // örneğin update işleminden sonra data dönülmez
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
