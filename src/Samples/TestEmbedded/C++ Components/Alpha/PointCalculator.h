#pragma once
#include "PointCalculator.g.h"

namespace winrt::Alpha::implementation
{
    struct PointCalculator : PointCalculatorT<PointCalculator>
    {
        PointCalculator() = default;

        winrt::Windows::Foundation::Point Flip(winrt::Windows::Foundation::Point const& px);
        double Add(winrt::Windows::Foundation::Point const& px, winrt::Windows::Foundation::Point const& py);
    };
}
namespace winrt::Alpha::factory_implementation
{
    struct PointCalculator : PointCalculatorT<PointCalculator, implementation::PointCalculator>
    {
    };
}
