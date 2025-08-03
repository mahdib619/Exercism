#include <math.h>

#include <array>
#include <string>
#include <vector>

#define MIN_PASSING_SCORE 41

// Round down all provided student scores.
std::vector<int> round_down_scores(std::vector<double> student_scores) {
    std::vector<int> round_scores;

    for (auto &&score : student_scores) {
        round_scores.emplace_back(floor(score));
    }

    return round_scores;
}

// Count the number of failing students out of the group provided.
int count_failed_students(std::vector<int> student_scores) {
    int failed_students_count = 0;

    for (auto &&score : student_scores) {
        if (score < MIN_PASSING_SCORE) {
            failed_students_count++;
        }
    }

    return failed_students_count;
}

// Determine how many of the provided student scores were 'the best' based on
// the provided threshold.
std::vector<int> above_threshold(std::vector<int> student_scores, int threshold) {
    std::vector<int> above_threshold_scores;

    for (auto &&score : student_scores) {
        if (score >= threshold) {
            above_threshold_scores.emplace_back(score);
        }
    }

    return above_threshold_scores;
}

// Create a list of grade thresholds based on the provided highest grade.
std::array<int, 4> letter_grades(int highest_score) {
    int grade_range = (highest_score - (MIN_PASSING_SCORE - 1)) / 4;
    return {
        MIN_PASSING_SCORE + grade_range * 0,
        MIN_PASSING_SCORE + grade_range * 1,
        MIN_PASSING_SCORE + grade_range * 2,
        MIN_PASSING_SCORE + grade_range * 3};
}

// Organize the student's rank, name, and grade information in ascending order.
std::vector<std::string> student_ranking(std::vector<int> student_scores, std::vector<std::string> student_names) {
    std::vector<std::string> rankings;

    for (int i = 0; i < student_scores.size(); i++) {
        std::string score = std::to_string(student_scores.at(i));
        std::string rank = std::to_string(i + 1);

        rankings.emplace_back((rank + ". " + student_names.at(i) + ": " + score));
    }

    return rankings;
}

// Create a string that contains the name of the first student to make a perfect score on the exam.
std::string perfect_score(std::vector<int> student_scores, std::vector<std::string> student_names) {
    for (int i = 0; i < student_scores.size(); i++) {
        if (student_scores.at(i) == 100) {
            return student_names.at(i);
        }
    }

    return "";
}
