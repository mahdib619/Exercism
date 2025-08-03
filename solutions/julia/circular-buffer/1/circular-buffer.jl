mutable struct CircularBuffer{T} <: AbstractVector{T}
    items::Vector{T}
    currentposition::Int
    capacity::Int

    CircularBuffer{T}(capacity::Integer) where {T} = new(zeros(T, capacity), 0, capacity)
end

function Base.push!(cb::CircularBuffer, item; overwrite::Bool=false)
    cb.currentposition == 
end

function Base.popfirst!(cb::CircularBuffer)

end

function Base.empty!(cb::CircularBuffer)

end
