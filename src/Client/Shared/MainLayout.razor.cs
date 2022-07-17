using Microsoft.AspNetCore.Components;

namespace ReflectInput.Client.Shared;

public partial class MainLayout
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}