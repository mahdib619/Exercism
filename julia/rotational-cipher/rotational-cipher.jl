Base.in(item::Int, ranges::AbstractArray{UnitRange{Int}}) = any(rng -> in(item, rng), ranges)
rotateascii(asc, key) = (s = asc >= 97 ? 97 : 65; mod(mod(asc + key, s), 26) + s)
rotate(key::Int, chr::AbstractChar) = (ach = Int(chr); !in(ach, [65:90, 97:122]) ? chr : Char(rotateascii(ach, key)))
rotate(key::Int, txt::AbstractString) = join([rotate(key, ch) for ch in txt])

# Bonus
for i in 0:26
    eval(Meta.parse("macro R$(i)_str(str); rotate($i, str); end"))
end