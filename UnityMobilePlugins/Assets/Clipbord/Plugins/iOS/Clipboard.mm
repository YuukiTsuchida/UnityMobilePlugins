
extern "C"
{
    void _Copy(const char* text)
    {
        NSString* nsText = [NSString stringWithUTF8String:text];
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        [pasteboard setValue:nsText forPasteboardType:@"public.text"];
    }
}
