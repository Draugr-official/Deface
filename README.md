# Deface
 A .NET compiler directed towards compiling C# to machine code.
 Currently utilizes Linux interrupts, as of now.

# Usage
There is currently no command line syntax.

# Examples
Constant folding is disabled for the following snippets.
After inputting the following code;
```cs
class Program
{
        void Main()
        {
                if("hi" == "hi")
                {
                        Console.WriteLine("the same");
                }
                else
                {
                        Console.WriteLine("not the same");
                }
        }
}
```

Deface will output the following x86 assembly code in a object file;
```asm
global _start
section .text:
Console_WriteLine:
    mov ebx, 1
    mov eax, 4
    int 80h
    ret

_start:
    lea esi, [var0]
    lea edi, [var2]
    mov ecx, var1
    rep cmpsb
    jne var3
    mov ecx, var4
    mov edx, 8
    call Console_WriteLine
    jmp var5
var3:
    mov ecx, var6
    mov edx, 12
    call Console_WriteLine
var5:
    mov eax, 1
    int 80h
    ret

section .data:
    var0 db 'hi',0
    var1 equ $-var0
    var2 db 'hi',0
    var4 db 'the same',0
    var6 db 'not the same',0
```
Some names are preserved to help debugging
