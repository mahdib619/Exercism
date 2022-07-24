const NORTH, EAST, SOUTH, WEST = (0, 1), (1, 0), (0, -1), (-1, 0)

rights = Dict(NORTH => EAST, EAST => SOUTH, SOUTH => WEST, WEST => NORTH)
lefts = Dict([kv[2] => kv[1] for kv in rights])

struct Point
    x::Int
    y::Int
end
Point(xy::Tuple{Int,Int}) = Point(xy[1], xy[2])

mutable struct Robot
    pos::Point
    head::Tuple{Int,Int}
end
Robot(pos::Tuple{Int,Int}, head::Tuple{Int,Int}) = Robot(Point(pos), head)

Base.:+(a::Point, b::Point) = Point(a.x + b.x, a.y + b.y)

position(r::Robot) = r.pos
heading(r::Robot) = r.head

turn_right!(r::Robot) = (r.head = rights[r.head]; r)
turn_left!(r::Robot) = (r.head = lefts[r.head]; r)
advance!(r::Robot) = (r.pos += Point(r.head); r)
instmethods = Dict('A' => advance!, 'R' => turn_right!, 'L' => turn_left!)

move!(r::Robot, inst::AbstractString) = (mv! = reduce(∘, map(i -> instmethods[i], reverse(collect(inst))));mv!(r))