#ifndef GRADIENT_SHADER_OPTIMIZE
#define GRADIENT_SHADER_OPTIMIZE

void GradientBranch_float(float Predicate, Gradient True, Gradient False, out Gradient Out)
{
    if (Predicate > 0)
    {
        Out = True;
    }
    else
    {
        Out = False;
    }
}

bool isfinite(Gradient g)
{
    return true;
}
#endif
