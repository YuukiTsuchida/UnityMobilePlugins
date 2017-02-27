#include <cstring>

extern "C"
{
    void _Copy(const char* text)
    {
        NSString* nsText = [NSString stringWithUTF8String:text];
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        [pasteboard setValue:nsText forPasteboardType:@"public.text"];
    }

    char* _Paste()
    {
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        NSString *string = [pasteboard valueForPasteboardType:@"public.text"];
        const char* cStr = [string UTF8String];
        char* res = (char*)malloc(strlen(cStr) + 1);
        std::strcpy(res, cStr);
        return res;
    }
}
