function encode(s)
    length(s) < 2 && return s

    s *= "."
    ccount, cchar, output = 1, s[begin], ""

    for c in s[2:end]
        if cchar == c
            ccount += 1
        else
            output *= "$(ccount > 1 ? ccount : "")$cchar"
            ccount, cchar = 1, c
        end
    end

    return output
end

function decode(s::AbstractString)
    output = cn = ""

    for c in s
        if !isnumeric(c)
            cn = isempty(cn) ? "1" : cn
            output *= c ^ parse(Int, cn)
            cn = ""
        else
            cn *= c
        end
    end

    return output
end