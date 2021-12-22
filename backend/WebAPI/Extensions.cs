using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI;

public static class Extensions
{
    public static IActionResult ToResponse(this CQRSResponse response)
    {
        if (response.IsUnsuccessful)
            return new ObjectResult(new {response.ErrorMessage}) {StatusCode = response.StatusCode};

        if (response.HasData)
            return new ObjectResult(new {Data = response.GetData()}) {StatusCode = response.StatusCode};

        return new StatusCodeResult(response.StatusCode);
    }
}