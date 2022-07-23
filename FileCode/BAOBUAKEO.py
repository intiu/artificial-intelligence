import random
import time

game_still_going = True
player_points = 0
computer_points = 0
name = input('Nhập tên của bạn: ')
print('Xin chào ' + name + ' \n Hãy chơi trò chơi bao búa kéo!')
print('Đó là trò chơi bao búa kéo xem bạn với máy tính ai có 3 điểm là thắng!')

while game_still_going == True:
    print('r = búa, p = bao, s = kéo')
    player_move = input('Chọn bao búa kéo: ')
    print('Máy tính đang chọn......')
    time.sleep(2.0)
    options = ['r', 'p', 's']
    computer_move = random.choice(options)

    if computer_move == 'p' and player_move == 'r':
        print('Bạn chọn búa')
        print('Máy tính chọn bao')
        computer_points += 1
        print('Máy tính đã thắng vòng này!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    elif computer_move == 's' and player_move == 'p':
        print('Bạn chọn bao')
        print('Máy tính chọn kéo')
        computer_points += 1
        print('Máy tính đã thắng vòng này!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    elif computer_move == 'r' and player_move == 's':
        print('Bạn chọn kéo')
        print('Máy tính chọn búa')
        computer_points += 1
        print('Máy tính đã thắng vòng này!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    elif computer_move == 'r' and player_move == 'r':
        print('Bạn chọn búa')
        print('Máy tính chọn búa')
        print('Đó là hòa!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    elif computer_move == 's' and player_move == 's':
        print('Bạn chọn kéo')
        print('Máy tính chọn kéo')
        print('Đó là hòa!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    elif computer_move == 'p' and player_move == 'p':
        print('Bạn chọn bao')
        print('Máy tính chọn bao')
        print('Đó là hòa!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    elif computer_move == 'r' and player_move == 'p':
        print('Bạn chọn bao')
        print('Máy tính chọn búa')
        player_points += 1
        print('Bạn đã thắng vòng này!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    elif computer_move == 's' and player_move == 'r':
        print('Bạn chọn búa')
        print('Máy tính chọn kéo')
        player_points += 1
        print('Bạn đã thắng vòng này!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    elif computer_move == 'p' and player_move == 's':
        print('Bạn chọn kéo')
        print('Máy tính chọn bao')
        player_points += 1
        print('Bạn đã thắng vòng này!')
        print('Bạn có ' + str(player_points) + ' điểm.')
        print('Máy tính có ' + str(computer_points) + ' điểm.')

    else:
        print('Không hợp lệ! Thử lại')

    if player_points == 3:
        print('Làm tốt lắm ' + name + ' , Bạn đã thắng!')
        game_still_going = False

    elif computer_points == 3:
        print('Máy tính đã thắng, chúc bạn may mắn lần sau ' + name + ' !')
        game_still_going = False
        

