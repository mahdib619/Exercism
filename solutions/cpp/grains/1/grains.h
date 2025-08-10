#if !defined(GRAINS_H)
#define GRAINS_H

namespace grains {
unsigned long long square(int square_number) {
    return 1ULL << (square_number - 1);
}
unsigned long long total() {
    return square(65);
}
}  // namespace grains

#endif  // GRAINS_H
