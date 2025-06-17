using AutoCamp.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoCamp.domain
{
    public class CampBpDomain
    {
        public async static Task<string> publicBpCamp(string cookie, string fb_dtsg, string idTkqc, string idPage, string idPost, string? proxy = null)
        {
            string uid = HelperUtils.ExtractUserIdFromCookie(cookie);

            string flowId = await getFlowId(cookie, idPage, idPost, idTkqc, proxy);

            var options = new RestClientOptions("https://www.facebook.com")
            {
                MaxTimeout = -1,
            };

            if (!string.IsNullOrEmpty(proxy))
            {
                ProxyHelper.SetProxy(options, proxy);
            }


            var client = new RestClient(options);
            var request = new RestRequest("/api/graphql/", Method.Post);
            request.AddHeader("accept", "*/*");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("priority", "u=1, i");
            request.AddHeader("sec-fetch-mode", "cors");
            request.AddHeader("sec-fetch-site", "same-origin");
            request.AddHeader("referer", $"https://www.facebook.com/ad_center/create/boostpost/?ad_account_id={idTkqc}&entry_point=www_profile_plus_permalink&page_id={idPage}&target_id={idPost}");
            request.AddHeader("Cookie", cookie);
            request.AddParameter("av", uid);
            request.AddParameter("__aaid", idTkqc);
            request.AddParameter("__user", uid);
            request.AddParameter("__a", "1");
            request.AddParameter("fb_dtsg", fb_dtsg);
            request.AddParameter("variables", "{\"input\":{\"boost_id\":null,\"creation_spec\":{\"ab_test_audiences\":[{\"audience_option\":\"AUTO_TARGETING\",\"saved_audience_id\":null,\"targeting_spec_string\":\"{\\\"genders\\\":[0],\\\"age_min\\\":18,\\\"age_max\\\":65,\\\"geo_locations\\\":{\\\"countries\\\":[\\\"VN\\\"],\\\"location_types\\\":[\\\"home\\\",\\\"recent\\\"]},\\\"targeting_optimization\\\":\\\"expansion_all\\\",\\\"targeting_automation\\\":{\\\"advantage_audience\\\":1}}\"}],\"ads_lwi_goal\":\"AUTOMATIC\",\"audience_option\":\"AUTO_TARGETING\",\"auto_boost_settings_id\":null,\"auto_targeting_sources\":[],\"billing_event\":\"IMPRESSIONS\",\"budget\":300,\"budget_type\":\"DAILY_BUDGET\",\"currency\":\"EUR\",\"dsa_beneficiary\":\"\",\"dsa_payor\":\"\",\"duration_in_days\":7,\"enable_clo\":false,\"is_automatic_goal\":true,\"is_gen_ai_media\":false,\"legacy_ad_account_id\":\""+idTkqc+"\",\"legacy_entry_point\":\"www_profile_plus_permalink\",\"logging_spec\":{\"reach_estimates\":{\"lower_estimates\":5150,\"upper_estimates\":14886},\"spec_history\":[{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":200,\"currency\":\"EUR\"},{\"budget\":300,\"currency\":\"EUR\"}]},\"messenger_welcome_message\":{\"call_prompt_message\":\"Call now for faster service.\",\"greeting\":\"Hi {{user_first_name}}! Please let us know how we can help you.\",\"icebreakers\":[\"Can I learn more about your business?\",\"Can you tell me more about your ad?\",\"Is anyone available to chat?\"],\"icebreakers_enabled\":true,\"is_call_prompt_enabled\":false,\"lwi_web_ai_generated_icebreakers_enabled\":false,\"prefill\":\"Hello! Can I get more info on this?\",\"prefill_enabled\":false,\"responses\":[\"\",\"\",\"\"]},\"partner_app_welcome_message\":null,\"pixel_event_type\":null,\"pixel_id\":null,\"placement_spec\":{\"publisher_platforms\":[\"FACEBOOK\"]},\"regional_regulated_categories\":[],\"regulated_categories\":[],\"regulated_category\":\"NONE\",\"retargeting_enabled\":false,\"run_continuously\":false,\"saved_audience_id\":null,\"singapore_universal_beneficiary_id\":null,\"singapore_universal_payer_id\":null,\"special_ad_category_countries\":[],\"start_time\":null,\"surface\":null,\"targeting_spec_string\":\"{\\\"genders\\\":[0],\\\"age_min\\\":18,\\\"age_max\\\":65,\\\"geo_locations\\\":{\\\"countries\\\":[\\\"VN\\\"],\\\"location_types\\\":[\\\"home\\\",\\\"recent\\\"]},\\\"targeting_optimization\\\":\\\"expansion_all\\\",\\\"targeting_automation\\\":{\\\"advantage_audience\\\":1}}\",\"adgroup_specs\":[{\"creative\":{\"body\":\"Cây xanh mướt\",\"call_to_action\":{\"type\":\"MESSAGE_PAGE\",\"value\":{\"app_destination\":\"MESSENGER\",\"link\":\"https://fb.com/messenger_doc/\"}},\"degrees_of_freedom_spec\":{\"degrees_of_freedom_type\":\"USER_ENROLLED_LWI_ACO\"},\"object_story_id\":\""+idPage+"_"+idPost+ "\",\"use_page_actor_override\":null}}],\"cta_data\":{\"is_cta_share_post\":false,\"link\":\"https://fb.com/messenger_doc/\",\"type\":\"MESSAGE_PAGE\"},\"objective\":\"MESSAGES\"},\"external_dependent_ent_id\":null,\"flow_id\":\""+flowId+"\",\"lwi_asset_id\":{\"id\":\"" + idPage+"\"},\"manual_review_requested\":false,\"page_id\":\""+idPage+"\",\"product\":\"BOOSTED_POST\",\"target_id\":\""+idPost+"\",\"actor_id\":\""+uid+"\",\"client_mutation_id\":\"1\"}}");
            request.AddParameter("server_timestamps", "true");
            request.AddParameter("doc_id", "9955578997835249");
            RestResponse response = await client.ExecuteAsync(request);


            return response.Content.Contains("CREATING") ? "Camp Bp thành công!" : "Lỗi camp bp...";
        }


        public async static Task<string> getFlowId(string cookie, string idPage, string idPost, string idTkqc, string? proxy = null)
        {
            var options = new RestClientOptions("https://www.facebook.com")
            {
                MaxTimeout = -1,
            };

            if (!string.IsNullOrEmpty(proxy))
            {
                ProxyHelper.SetProxy(options, proxy);
            }

            var client = new RestClient(options);
            var request = new RestRequest($"/ad_center/create/boostpost/?entry_point=www_profile_plus_timeline_caa_cae_voice&page_id={idPage}&target_id={idPost}&ad_account_id={idTkqc}", Method.Get);
            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.AddHeader("dpr", "1");
            request.AddHeader("priority", "u=0, i");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("sec-ch-ua-platform-version", "\"10.0.0\"");
            request.AddHeader("sec-fetch-dest", "document");
            request.AddHeader("sec-fetch-site", "same-origin");
            request.AddHeader("sec-fetch-user", "?1");
            request.AddHeader("Cookie", cookie);
            RestResponse response = await client.ExecuteAsync(request);

            string responseContent = response.Content;

            string pattern = @"flow_id""\s*:\s*""([a-f0-9\-]+)";

            Match match = Regex.Match(responseContent, pattern);

            if (match.Success)
            {
                string flowId = match.Groups[1].Value;
                return flowId;
            }


            return "Lỗi lấy flow id"; // Chưa có logic để lấy flowId từ response
        }

    }
}
