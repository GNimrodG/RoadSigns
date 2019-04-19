var Signs = [];
var FadeEffects = true;
var FlashEffect = true;
var SignPillar = true;

class Sign {
    constructor(type, useText) {
        this.Type = type;
        this.UseText = useText;
        this.Main = $("<div id=\"" + type + "Sign\" class=\"Sign\" style=\"display: none;\"></div>");
        if (this.UseText) {
            this.Text = $("<div id=\"" + type + "Text\" class=\"Text\"></div>")
            this.Main.append(this.Text);
        } else {
            this.Main.addClass("Under");
        }

        if (SignPillar) {
            this.Pillar = $("<div class=\"SignPillar\"></div>");
            this.Main.append(this.Pillar);
        }

        this.Number = $("<div id=\"" + type + "Number\" class=\"Number\"></div>");
        this.Main.append(this.Number);
        this.Divider = $("<div class=\"divider\" style=\"display: none;\"></div>");
        $("#container").append(this.Main);
        $("#container").append(this.Divider);
    }

    Show(Limit, Text) {
        this.Number.text(Limit);
        if (Limit > 99) {
            this.Main.addClass("Over");
        } else {
            this.Main.removeClass("Over");
        }
        if (this.UseText) {
            this.Text.text(Text);
        }
        var sign = this;
        if (FadeEffects) {
            this.Divider.fadeIn();
            this.Main.fadeIn(1000, function () {
                if (FlashEffect) {
                    sign.Main.addClass("Animate");
                    setTimeout(function () { sign.Main.removeClass("Animate"); }, 5000);
                }
            });
        } else {
            this.Divider.fadeIn(0);
            this.Main.fadeIn(0, function () {
                if (FlashEffect) {
                    sign.Main.addClass("Animate");
                    setTimeout(function () { sign.Main.removeClass("Animate"); }, 5000);
                }
            });
        }
    }

    Hide() {
        if (FadeEffects) {
            this.Main.fadeOut(1000);
            this.Divider.fadeOut();
        } else {
            this.Main.fadeOut(0);
            this.Divider.fadeOut(0);
        }
    }

    Remove(){
        this.Main.Remove();
    }
}

window.addEventListener("message", HandleMessage);

function HandleMessage(event) {
    var data = event.data;

    if (data.Create) {
        var sign = new Sign(data.Type, data.UseText);
        Signs[data.Type] = sign;
    }

    if (data.Show) {
        Signs[data.Type].Show(data.Limit, data.Text);
    }

    if (data.Hide) {
        Signs[data.Type].Hide();
    }

    if (data.Config) {
        switch (data.Corner) {
            default:
            case 0:
                break;
            case 1:
                $("#container").addClass("Right");
                break;
            case 2:
                $("#container").addClass("Bottom");
                break;
            case 3:
                $("#container").addClass("Bottom Right");
                break;
        }

        FadeEffects = data.FadeEffects;
        FlashEffect = data.FlashEffect;

        SignPillar = data.SignPillar;
    }
}