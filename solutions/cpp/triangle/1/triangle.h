#if !defined(TRIANGLE_H)
#define TRIANGLE_H

#include <stdexcept>

namespace triangle {
enum class flavor {
    equilateral,
    isosceles,
    scalene
};

bool is_triangle(float a, float b, float c) {
    if (a + b + c == 0 || a + b < c || b + c < a || a + c < b) {
        return false;
    }
    return true;
}
flavor kind(float a, float b, float c) {
    if (!is_triangle(a, b, c)) {
        throw std::domain_error("invalid sides!");
    }

    if (a == b && b == c) {
        return flavor::equilateral;
    }
    if (a == b || b == c || a == c) {
        return flavor::isosceles;
    }
    return flavor::scalene;
}
}  // namespace triangle

#endif  // TRIANGLE_H