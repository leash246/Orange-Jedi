function SetAbilityLabel(ddlID) {
    var ControlName = document.getElementById(ddlID.id);
    var AbilityScore = ControlName.value;
    var AbilityModifier = Math.floor((AbilityScore - 10) / 2);
    var ModiferLabel = document.getElementById(ddlID.id.replace("ddl", "lbl"));
    ModiferLabel.textContent = AbilityModifier;
    var txtID = document.getElementById(ddlID.id.replace("ddl", "txt") + "Temp");
    SetTempAbilityLabel(txtID);
}
function SetTempAbilityLabel(txtID) {
    var txtAbility = document.getElementById(txtID.id);
    var ddlAbility = document.getElementById(txtID.id.replace("txt", "ddl").replace("Temp", ""));
    var AbilityScore = ddlAbility.value;
    var TempScore = 0;
    var lblTemp = document.getElementById(txtID.id.replace("txt", "lbl"));
    if (txtAbility.value != "" && txtAbility.value != 0) { 
        TempScore = txtAbility.value;
        TempScore = parseInt(TempScore) + parseInt(AbilityScore);
        lblTemp.textContent = Math.floor((TempScore - 10) / 2);
    } else {
        lblTemp.textContent = "";
    }
    if (txtAbility.title == "Dex") {
        ChangedAC(txtID);
        __doPostBack('UpdateSaves');
    } else if (txtAbility.title == "Wis") {
        __doPostBack('UpdateSaves');
    } else if (txtAbility.title == "Con") {
        __doPostBack('UpdateSaves');
    } else if (txtAbility.title == "Str") {
        if (lblTemp.textContent == "") {
            SetGrapple('Str', lblTemp.textContent);
        } else {
            var AbilityModifier = Math.floor((AbilityScore - 10) / 2);
            SetGrapple('Str', AbilityModifer);
        }
    }
    
}
function ChangedHP(hpID) {
    var changedItem = document.getElementById(hpID.id);
    __doPostBack('UpdateHP', hpID.id);
}
function ChangedAC(acID) {
    var changedItem = document.getElementById(acID.id);
    __doPostBack('UpdateAC', acID.id);
}
function SetInitiativeLabel(txtID) {
    var changedItem = document.getElementById(txtID.id);
    var DexMod = document.getElementById(txtID.id.replace("txtMisc", "lblDex"));
    var InitMod = parseInt(changedItem.value) + parseInt(DexMod.textContent);
    var lblInitMod = document.getElementById(DexMod.id.replace("Dex", "") + "Mod");
    lblInitMod.textContent = InitMod;
}
function SetSavingThrow(txtID) {
    var changedItem = document.getElementById(txtID.id);
    var Mode = txtID;
    if (changedItem != null) {
        txtID = changedItem.title;
    }
    var TotalSave;
    var BaseSave;
    var AbilityMod;
    var MagicMod;
    var MiscMod;
    var TempMod;
    var MainContent = 'MainContent_ctrlSaves_'
    
    if (txtID == 'Fortitude') {
        BaseSave = document.getElementById(MainContent + 'txtFortBase').value;
        AbilityMod = document.getElementById(MainContent + 'lblConSave').textContent;
        MagicMod = document.getElementById(MainContent + 'txtMagicModFort').value;
        MiscMod = document.getElementById(MainContent + 'txtMiscModFort').value;
        TempMod = document.getElementById(MainContent + 'txtTempModFort').value;
        TotalSave = document.getElementById(MainContent + 'lblFortSave');
    } else if (txtID == 'Reflex') {
        BaseSave = document.getElementById(MainContent + 'txtRefBase').value;
        AbilityMod = document.getElementById(MainContent + 'lblDexSave').textContent;
        MagicMod = document.getElementById(MainContent + 'txtMagicModRef').value;
        MiscMod = document.getElementById(MainContent + 'txtMiscModRef').value;
        TempMod = document.getElementById(MainContent + 'txtTempModRef').value;
        TotalSave = document.getElementById(MainContent + 'lblRefSave');
    } else if (txtID == 'Will') {
        BaseSave = document.getElementById(MainContent + 'txtWillBase').value;
        AbilityMod = document.getElementById(MainContent + 'lblWisSave').textContent;
        MagicMod = document.getElementById(MainContent + 'txtMagicModWill').value;
        MiscMod = document.getElementById(MainContent + 'txtMiscModWill').value;
        TempMod = document.getElementById(MainContent + 'txtTempModWill').value;
        TotalSave = document.getElementById(MainContent + 'lblWillSave');
    }
    if (BaseSave == "") { BaseSave = "0"; }
    if (AbilityMod == "") { AbilityMod = "0"; }
    if (MagicMod == "") { MagicMod = "0"; }
    if (MiscMod == "") { MiscMod = "0"; }
    if (TempMod == "") { TempMod = "0"; }
    TotalSave.textContent = parseInt(BaseSave) + parseInt(AbilityMod) +
        parseInt(MagicMod) + parseInt(MiscMod) + parseInt(TempMod);
    
}
function SetGrapple(control, value) {
    var MainContent = 'MainContent_ctrlSaves_'
    var TotalGrapple = document.getElementById(MainContent + 'lblGrapple');
    var BaseAttack = document.getElementById(TotalGrapple.id + 'BaseAttack');
    var StrMod = document.getElementById(TotalGrapple.id + 'Str');
    var SizeMod = document.getElementById(TotalGrapple.id + 'Size');
    var MiscMod = document.getElementById(TotalGrapple.id.replace('lbl', 'txt') + 'Misc').value;

    if (control == "Size") {
        SizeMod.textContent = value;
    } else if (control == "BaseAttack") {
        BaseAttack.textContent = value;
    } else if (control == "Str") {
        StrMod.textContent = value;
    }

    var BaseAttackInt = BaseAttack.textContent;
    var StrModInt = StrMod.textContent;
    var SizeModInt = SizeMod.textContent;

    if (BaseAttackInt.textContent == "") { BaseAttackInt = "0"; }
    if (StrModInt == "") { StrModInt = "0"; }
    if (SizeModInt == "") { SizeModInt = "0"; }
    if (MiscMod == "") { MiscMod = "0"; }
    TotalGrapple.textContent = parseInt(BaseAttackInt) + parseInt(StrModInt) +
        parseInt(SizeModInt) + parseInt(MiscMod);

}