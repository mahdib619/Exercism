rotates = [(0, 1), (1, 0), (0, -1), (-1, 0)]

function spiral_matrix(n)
    cur,mat, (a, b) = 1,zeros(Int, n, n), (1, 0)
	
    for i = 1:n*n
        r = rotates[cur]
        (ta, tb) = (a + r[1], b + r[2])
        if !isassigned(mat, ta, tb) || mat[ta, tb] != 0
            cur = mod(cur, 4) + 1
            r = rotates[cur]
            (ta, tb) = (a + r[1], b + r[2])
        end
		a,b,mat[ta,tb] = ta,tb,i
    end

	return mat
end