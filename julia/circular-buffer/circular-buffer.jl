mutable struct CircularBuffer{T} <: AbstractVector{T}
    items::Vector{T}
    capacity::Int

    CircularBuffer{T}(capacity::Integer) where {T} = new(Vector{T}(), capacity)
end

isfull(cb::CircularBuffer) = length(cb) == cb.capacity
capacity(cb::CircularBuffer) = cb.capacity
Base.length(cb::CircularBuffer) = length(cb.items)
Base.size(cb::CircularBuffer) = (length(cb),)

Base.iterate(cb::CircularBuffer, state=1) = state > length(cb.items) ? nothing : (cb.items[state], state + 1)

Base.eltype(::CircularBuffer{T}) where {T} = T
Base.eltype(::Type{CircularBuffer{T}}) where {T} = T

Base.setindex!(cb::CircularBuffer, v, i) = cb.items[i] = v
Base.getindex(cb::CircularBuffer, i) = cb.items[i]
Base.lastindex(cb::CircularBuffer) = lastindex(cb.items)
Base.eachindex(cb::CircularBuffer) = eachindex(cb.items)

Base.empty!(cb::CircularBuffer) = (empty!(cb.items); cb)
Base.isempty(cb::CircularBuffer) = isempty(cb.items)

Base.pop!(cb::CircularBuffer) = isempty(cb) ? throw(BoundsError()) : pop!(cb.items)
Base.popfirst!(cb::CircularBuffer) = isempty(cb) ? throw(BoundsError()) : popfirst!(cb.items)

Base.push!(cb::CircularBuffer, item; overwrite::Bool=false) = innerpush!(cb, item; overwrite=overwrite)
Base.pushfirst!(cb::CircularBuffer, item; overwrite::Bool=false) = innerpush!(cb, item; (in!)=pushfirst!, (out!)=pop!, overwrite=overwrite)

append!(cb::CircularBuffer, vals::AbstractVector; overwrite::Bool=false) = push!.((cb,), vals; overwrite)

function innerpush!(cb::CircularBuffer, item; (in!)=push!, (out!)=popfirst!, overwrite=false)
    isfull(cb) && (overwrite ? out!(cb) : throw(BoundsError()))
    in!(cb.items, item)
    return cb
end