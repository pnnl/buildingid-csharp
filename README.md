# Unique Building Identification (UBID)

**Website:** https://buildingid.pnnl.gov/

## Documentation

### Install

The .NET Standard assembly of this project is published to the [NuGet](https://nuget.org/) package repository.

Visit https://nuget.org/packages/buildingid-csharp or add the following `<PackageReference/>` to your project file:

```xml
<ItemGroup>
  <PackageReference Include="buildingid-csharp" Version="1.0.0" />
</ItemGroup>
```

## Usage

The `buildingid-csharp` package is a class library.

### The API

* `PNNL.BuildingId.Code`
  * Static methods:
    * `Encode(float, float, float, float, float, float, int) : PNNL.BuildingId.Code`
  * Instance methods:
    * `Decode() : PNNL.BuildingId.CodeArea`
    * `IsValid() : bool`
* `PNNL.BuildingId.CodeArea`
  * Instance methods:
    * `Encode() : PNNL.BuildingId.Code`
    * `Resize() : PNNL.BuildingId.CodeArea`

In the following example, a UBID code is decoded and then re-encoded:

```csharp
using PNNL.BuildingId;
using System;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize UBID code.
            Code code = new Code("849VQJH6+95J-51-58-42-50");

            // Is the UBID code valid?
            if (code.IsValid())
            {
                try
                {
                    // Decode the UBID code.
                    CodeArea codeArea = code.Decode();

                    // Resize the resulting UBID code area.
                    //
                    // The effect of this operation is that the height and width of
                    // the UBID code area are reduced by half an OLC code area.
                    CodeArea newCodeArea = codeArea.Resize();

                    // Encode the new UBID code area.
                    Code newCode = newCodeArea.Encode();

                    // Test that the new UBID code matches the original.
                    Console.WriteLine(code.Value == newCode.Value);
                }
                catch (InvalidCodeException reason)
                {
                    Console.WriteLine($"Call to Decode() method failed: {reason}");
                }
            }
            else
            {
                Console.WriteLine("UBID code is invalid.")
            }
        }
    }
}
```

## License

The gem is available as open source under the terms of [The 2-Clause BSD License](https://opensource.org/licenses/BSD-2-Clause).

## Contributions

Contributions are accepted on [GitHub](https://github.com/) via the fork and pull request workflow. See [here](https://help.github.com/articles/using-pull-requests/) for more information.
