names = Set{AbstractString}()
randomstr(l, ascrange) = join([Char(rand(ascrange)) for i in 1:l])
function getrandomname()
    while true
        n = randomstr(2, 65:90) * randomstr(3, 48:57)
        if !in(n, names)
            push!(names, n)
            return n
        end
    end
end
mutable struct Robot
    name::AbstractString
    Robot() = new(getrandomname())
end
reset!(instance::Robot) = instance.name = getrandomname()
name(instance::Robot) = instance.name;