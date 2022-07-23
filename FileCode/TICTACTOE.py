import random  
def drawBoard(board):
      print('   |   |')
      print(' ' + board[1] + ' | ' + board[2] + ' | ' + board[3])
      print('   |   |')
      print('-----------')
      print('   |   |')
      print(' ' + board[4] + ' | ' + board[5] + ' | ' + board[6])
      print('   |   |')
      print('-----------')
      print('   |   |')
      print(' ' + board[7] + ' | ' + board[8] + ' | ' + board[9])
      print('   |   |')
def inputPlayerLetter():
      letter = ''
      while not (letter == 'X' or letter == 'O'):
          print('Bạn chọn X hay O?')
          letter = input().upper()
      if letter == 'X':
          return ['X', 'O']
      else:
          return ['O', 'X'] 
def whoGoesFirst():
      if random.randint(0, 1) == 0:
          return 'Máy Tính'
      else:
          return 'Người Chơi'
def playAgain():
      print('Bạn có muốn chơi lại không? (yes hoặc no)')
      return input().lower().startswith('y')
def makeMove(board, letter, move):
      board[move] = letter 
def isWinner(bo, le):
      return ((bo[7] == le and bo[8] == le and bo[9] == le) or 
      (bo[4] == le and bo[5] == le and bo[6] == le) or 
      (bo[1] == le and bo[2] == le and bo[3] == le) or 
      (bo[7] == le and bo[4] == le and bo[1] == le) or 
      (bo[8] == le and bo[5] == le and bo[2] == le) or 
      (bo[9] == le and bo[6] == le and bo[3] == le) or 
      (bo[7] == le and bo[5] == le and bo[3] == le) or 
      (bo[9] == le and bo[5] == le and bo[1] == le)) 
def getBoardCopy(board):
      dupeBoard = [] 
      for i in board:
         dupeBoard.append(i) 
      return dupeBoard
def isSpaceFree(board, move):
      return board[move] == ' ' 
def getPlayerMove(board):
      move = ' '
      while move not in '1 2 3 4 5 6 7 8 9'.split() or not isSpaceFree(board, int(move)):
         print('Bước tiếp theo của bạn là gì? (1-9)')
         move = input()
      return int(move) 
def chooseRandomMoveFromList(board, movesList):
      possibleMoves = []
      for i in movesList:
         if isSpaceFree(board, i):
             possibleMoves.append(i)
      if len(possibleMoves) != 0:
         return random.choice(possibleMoves)
      else:
         return None
def getComputerMove(board, computerLetter):
      if computerLetter == 'X':
         playerLetter = 'O'
      else:
         playerLetter = 'X'
      for i in range(1, 10):
         copy = getBoardCopy(board)
         if isSpaceFree(copy, i):
             makeMove(copy, computerLetter, i)
             if isWinner(copy, computerLetter):
                 return i
      for i in range(1, 10):
         copy = getBoardCopy(board)
         if isSpaceFree(copy, i):
             makeMove(copy, playerLetter, i)
             if isWinner(copy, playerLetter):
                 return i
      move = chooseRandomMoveFromList(board, [1, 3, 7, 9])
      if move != None:
         return move
      if isSpaceFree(board, 5):
         return 5
      return chooseRandomMoveFromList(board, [2, 4, 6, 8])
def isBoardFull(board):
      for i in range(1, 10):
         if isSpaceFree(board, i):
             return False
      return True
      print('Chào mừng bạn đến với Tic Tac Toe!')
while True:
      theBoard = [' '] * 10
      playerLetter, computerLetter = inputPlayerLetter()
      turn = whoGoesFirst()
      print(turn + ' sẽ đi trước.')
      gameIsPlaying = True
      while gameIsPlaying:
         if turn == 'Người Chơi':
             drawBoard(theBoard)
             move = getPlayerMove(theBoard)
             makeMove(theBoard, playerLetter, move)
             if isWinner(theBoard, playerLetter):
                 drawBoard(theBoard)

                 print('Hoan hô! Bạn đã thắng trò chơi!')
                 gameIsPlaying = False
             else:
                 if isBoardFull(theBoard):
                     drawBoard(theBoard)
                     print('Game đấu hòa nhau!')
                     break
                 else:
                     turn = 'Máy Tính'              
         else:
             move = getComputerMove(theBoard, computerLetter)
             makeMove(theBoard, computerLetter, move)
             if isWinner(theBoard, computerLetter):
                 drawBoard(theBoard)
                 print('Máy tính đã đánh bại bạn! Bạn đã thua.')
                 gameIsPlaying = False
             else:
                 if isBoardFull(theBoard):
                     drawBoard(theBoard)
                     print('Game đấu hòa nhau!')
                     break
                 else:
                     turn = 'Người Chơi'
      if not playAgain():
         break
