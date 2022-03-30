#include "pch.h"
#include "QIAgent.h"
#include "QIAgent.g.cpp"

namespace winrt::Beta::implementation
{
    bool QIAgent::CheckForIGamma(winrt::Alpha::IAlpha const& alpha)
    {
        return alpha.try_as<winrt::Gamma::IGamma>() ? true : false;
    }

    Windows::Media::AudioFrame QIAgent::ReturnsAudioFrame()
    {
        // make a buffer of 20 bytes
        return Windows::Media::AudioFrame(20);
    }

/*
    winrt::Alpha::IAlpha QIAgent::IdentityAlpha(winrt::Alpha::IAlpha const& alpha)
    {
        return alpha;
    }
*/
    int32_t QIAgent::Run(winrt::Alpha::IAlpha const& alpha)
    {
        return alpha.Five();
    }
}
