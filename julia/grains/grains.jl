throwifoutofrange(square) = (square < 1 || square > 64) && throw(DomainError(square))
on_square(square) = throwifoutofrange(square) || 2^(big(square - 1))
total_after(square) = throwifoutofrange(square) || sum(on_square.(collect(1:square)))