#include <Windows.h>

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);

HWND hTextBox;
HWND hButton;

HBRUSH hBackgroundBrush;

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    const wchar_t CLASS_NAME[] = L"Win32App";
    const wchar_t WINDOW_TITLE[] = L"Window1";

    // window registration
    WNDCLASSEX wcex = { 0 };
    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.hInstance = hInstance;
    wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
    wcex.lpszClassName = CLASS_NAME;
    RegisterClassEx(&wcex);

    // main window
    HWND hwnd = CreateWindowEx(
        0,
        CLASS_NAME,
        WINDOW_TITLE,
        WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
        NULL,
        NULL,
        hInstance,
        NULL
    );

    if (hwnd == NULL)
    {
        MessageBox(NULL, L"fatal eror - no window", L"eror", MB_OK | MB_ICONERROR);
        return 0;
    }

    // Создание текстового поля
    hTextBox = CreateWindowEx(
        0,
        L"EDIT",
        L"",
        WS_CHILD | WS_VISIBLE | WS_BORDER | ES_AUTOHSCROLL,
        0, 0, 200, 25,
        hwnd,
        NULL,
        hInstance,
        NULL
    );

    // btn
    hButton = CreateWindow(
        L"BUTTON",
        L"click!",
        WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
        0, 0, 100, 30,
        hwnd,
        NULL,
        hInstance,
        NULL
    );

    // window background
    hBackgroundBrush = CreateSolidBrush(RGB(199, 255, 255));

    // lessssss go
    ShowWindow(hwnd, nCmdShow);
    UpdateWindow(hwnd);

    // general msg loop
    MSG msg = { 0 };
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int)msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_CREATE:
        return 0;

    case WM_PAINT:
    {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hwnd, &ps);
        return 0;
    }
    // event btn_click()
    case WM_COMMAND:
        if (HIWORD(wParam) == BN_CLICKED)
        {
            if ((HWND)lParam == hButton)
            {
                int len = GetWindowTextLength(hTextBox) + 1;
                wchar_t* text = new wchar_t[len];
                GetWindowText(hTextBox, text, len);
                MessageBox(hwnd, text, L"ur text", MB_OK);
                delete[] text;
            }
        }
        return 0;

    case WM_CLOSE:
        DestroyWindow(hwnd);
        return 0;

    case WM_DESTROY:
        PostQuitMessage(0);
        return 0;

        //for cute background color ☺☻
    case WM_ERASEBKGND:
    {
        HDC hdc = (HDC)wParam;
        RECT rect;
        GetClientRect(hwnd, &rect);
        FillRect(hdc, &rect, hBackgroundBrush);
        return TRUE;
    }

    //  !!if ignore this we will have black borber when we max / min - imazing window
    case WM_SIZE:
    {
        int width = LOWORD(lParam);
        int height = HIWORD(lParam);

        //  center aligment!!
        int textBoxX = (width - 200) / 2;
        int textBoxY = (height - 25) / 2;
        SetWindowPos(hTextBox, NULL, textBoxX, textBoxY, 200, 25, SWP_NOZORDER);

        int buttonX = (width - 100) / 2;
        int buttonY = textBoxY + 30;
        SetWindowPos(hButton, NULL, buttonX, buttonY, 100, 30, SWP_NOZORDER);

        return 0;
    }

    default:
        //  other msgs
        return DefWindowProc(hwnd, msg, wParam, lParam);
    }
}