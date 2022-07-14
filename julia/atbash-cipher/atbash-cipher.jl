encode(input::AbstractString, chunk=5) = join(join.(Iterators.partition(filter(((!) ∘ isnothing), encode.(collect(lowercase(input)))), chunk)), " ")
encode(input::AbstractChar) = isnumeric(input) ? input : isletter(input) ? Char(219 - Int(input)) : nothing
decode(input) = encode(input, length(input))