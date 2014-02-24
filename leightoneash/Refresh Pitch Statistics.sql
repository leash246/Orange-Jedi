
delete from Cards.PitchTeammateStatistics
GO
declare @Data table(nPlayerID int,nGameSize int,nOtherPlayerID int,lTeammates int,lWin int,nGame int)
declare @nGame int = 0, @nPlayerID int = 18
	declare @nBuilding int = 3024;
WHILE @nPlayerID > 0 BEGIN
	SET @nBuilding = CASE WHEN @nPlayerID <= 8 THEN 3333 ELSE 3024 END
select @nGame = MAX(nGame) FROM Cards.PitchRankingsGraphData WHERE nBuilding = @nBuilding
WHILE @nGame > 0 BEGIN
;
	WITH PlayerData AS (Select CASE WHEN GD2.nELO = GD.nELO OR GDO2.nELO = GDO.nELO THEN 'NONE'
					WHEN GD2.nELO > GD.nELO AND GDO2.nELO > GDO.nELO THEN 'TEAMMATES'
					WHEN GD2.nELO < GD.nELO AND GDO2.nELO < GDO.nELO THEN 'TEAMMATES'
					ELSE 'OPPONENTS' END AS cStatus,
					CASE WHEN GD.nELO > GD2.nELO THEN 1 ELSE 0 END as lWin,
					@nGame as nGame,
					PR.nPlayerID as nPlayerID,GDO.nPlayerID as nOtherPlayer
	 from Cards.PitchRankings PR
		inner join Cards.PitchRankingsGraphData GD on GD.nPlayerID = PR.nPlayerID
			inner join Cards.PitchRankingsGraphData GD2 on GD2.nPlayerId = GD.nPlayerID and GD2.nGame = (GD.nGame - 1)
		inner join Cards.PitchRankingsGraphData GDO on GDO.nPlayerID <> GD.nPlayerID and GDO.nGame = GD.nGame AND GDO.nBuilding = GD.nBuilding
			inner join Cards.PitchRankingsGraphData GDO2 on GDO2.nPlayerId = GDO.nPlayerID and GDO2.nGame = (GDO.nGame - 1)
		
	WHERE GD.nGame = @nGame and 
	PR.nPlayerID = @nPlayerID and 
	GD.nBuilding = @nBuilding
	AND GD2.nELO <> GD.nELO)
	INSERT INTO @Data 
	SELECT nPlayerID,
		convert(int,(SELECT COUNT(*) FROM PlayerData WHERE cStatus <> 'NONE') + 1),
		nOtherPlayer,
		CASE WHEN cStatus = 'TEAMMATES' THEN 1 ELSE 0 END,
		CASE WHEN cStatus = 'TEAMMATES' THEN lWin ELSE 0 END,nGame
		FROM PlayerData
	--Select 'INSERT INTO @Data VALUES(' + convert(varchar,nPlayerID) + ',' + convert(varchar,(SELECT COUNT(*) FROM PlayerData WHERE cStatus <> 'NONE') + 1) + ',' +
	--	convert(varchar,nOtherPlayer) + ',' + CASE WHEN cStatus = 'TEAMMATES' THEN '1' ELSE '0' END + ',' + convert(varchar,lWin) + ')' from PlayerData
	order by nOtherPlayer
	SET @nGame = @nGame - 1
	END
	SET @nPlayerID = @nPlayerID - 1
END
--Select * from Cards.PitchRankings
update @Data set nGameSize = 4 WHERE nGameSize = 3
update @Data set nGameSize = 6 WHERE nGameSize = 5
insert into Cards.PitchTeammateStatistics
select nPlayerID,nGameSize,nOtherPlayerID,sum(lTeammates),sum(lWin) from @Data 
WHERE lTeammates = 1
group by nPlayerID,nGameSize,nOtherPlayerID

GO

Select * from Cards.PitchOpponentStatistics

