#include "pch.h"
#include "PointCalculator.h"
#include "PointCalculator.g.cpp"

namespace winrt::Alpha::implementation
{
    winrt::Windows::Foundation::Point PointCalculator::Flip(winrt::Windows::Foundation::Point const& px)
    {
        return winrt::Windows::Foundation::Point(-px.X, -px.Y);
    }
    double PointCalculator::Add(winrt::Windows::Foundation::Point const& px, winrt::Windows::Foundation::Point const& py)
    {
        return px.X + py.X;
    }
}
