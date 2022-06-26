using Dates

struct Clock
    hour::Int
    minute::Int

    function Clock(hour, minute)
        h = mod(hour + div(minute, 60), 24)
        m = mod(minute, 60)

        if (m > 0 && minute < 0)
            h -= 1
        end

        h = h >= 0 ? h : 24 + h
        new(h, m)
    end
end

Base.:+(c::Clock, m::Minute) = Clock(c.hour, c.minute + m.value)
Base.:-(c::Clock, m::Minute) = Clock(c.hour, c.minute - m.value)
Base.show(io::IO, x::Clock) = print(io, "\"" * string(x.hour; pad=2) * ":" * string(x.minute; pad=2) * "\"")